import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { take } from 'rxjs';
import { parsearErroresAPI } from '../utilidades/utilidades';
import { regionesProductoDTO } from './regiones.models';
import { RegionesService } from './regiones.service';

@Component({
  selector: 'app-regiones',
  templateUrl: './regiones.component.html',
  styleUrls: ['./regiones.component.css']
})
export class RegionesComponent implements OnInit {

  constructor(private regionesService: RegionesService) { }

  errores: string[] = [];
  regiones: regionesProductoDTO[];
  columnas = ['imagen','nombreProducto','regiones','precio','productosRelacionados','inventario','fechaCreacion','fechaModificacion','categoriaDescripcion','subcategoriaDescripcion' ,'acciones'];
  cantidadTotalRegistros;
  paginaActual = 1;
  cantidadRegistrosMostrados = 12;

  ngOnInit(): void {
    this.cargarRegistros(this.paginaActual,this.cantidadRegistrosMostrados);
  }

  cargarRegistros(pagina: number, cantidadElementosAMostrar){
    this.regionesService.obtenerRegiones(pagina, cantidadElementosAMostrar).pipe(take(1))
    .subscribe({
      next: res => {
        this.regiones = res.body;
        console.log(this.regiones);
        this.cantidadTotalRegistros = res.headers.get("cantidadTotalRegistros");
      },
      error: errores =>{
        this.regiones = new Array(0);
        this.errores = parsearErroresAPI(errores);
      }
    })
  }

  actualizarPaginacion(datos: PageEvent){
    this.paginaActual = datos.pageIndex + 1;
    this.cantidadRegistrosMostrados = datos.pageSize;
    this.cargarRegistros(this.paginaActual, this.cantidadRegistrosMostrados);
  }

}
