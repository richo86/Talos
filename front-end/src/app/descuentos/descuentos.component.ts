import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { take } from 'rxjs';
import Swal from 'sweetalert2';
import { parsearErroresAPI } from '../utilidades/utilidades';
import { descuentoDTO } from './descuentos.models';
import { DescuentosService } from './descuentos.service';

@Component({
  selector: 'app-descuentos',
  templateUrl: './descuentos.component.html',
  styleUrls: ['./descuentos.component.css']
})
export class DescuentosComponent implements OnInit {

  constructor(private descuentosService : DescuentosService) { }

errores: string[] = [];
descuentos: descuentoDTO[];
columnas = ['nombre','descripcion','estado','porcentajeDescuento','fechaCreacion','fechaEdicion','acciones'];
cantidadTotalRegistros;
paginaActual = 1;
cantidadRegistrosMostrados = 12;

ngOnInit(): void {
  this.cargarRegistros(this.paginaActual,this.cantidadRegistrosMostrados);
}

cargarRegistros(pagina: number, cantidadElementosAMostrar){
  this.descuentosService.obtenerDescuentos(pagina, cantidadElementosAMostrar).pipe(take(1))
  .subscribe({
    next: res => {
      this.descuentos = res.body;
      this.cantidadTotalRegistros = res.headers.get("cantidadTotalRegistros");
    },
    error: errores =>{
      this.errores = parsearErroresAPI(errores);
      this.descuentos = new Array(0);
    }
  })
}

actualizarPaginacion(datos: PageEvent){
  this.paginaActual = datos.pageIndex + 1;
  this.cantidadRegistrosMostrados = datos.pageSize;
  this.cargarRegistros(this.paginaActual, this.cantidadRegistrosMostrados);
}

borrar(id:string){
  this.descuentosService.borrarDescuento(id).pipe(take(1))
  .subscribe({
    next: () => {
    Swal.fire({
      text: '¡Operación exitosa!',
      icon: 'success'
    });
  },
  error: (error) => this.errores = parsearErroresAPI(error)}
  );
}

}
