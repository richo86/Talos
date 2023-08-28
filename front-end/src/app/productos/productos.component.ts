import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { take } from 'rxjs';
import Swal from 'sweetalert2';
import { dataURI, parsearErroresAPI } from '../utilidades/utilidades';
import { productoDTO } from './productos.models';
import { ProductosService } from './productos.service';

@Component({
  selector: 'app-productos',
  templateUrl: './productos.component.html',
  styleUrls: ['./productos.component.css']
})
export class ProductosComponent implements OnInit {

  constructor(private productosService : ProductosService,
    private router: Router) { }

errores: string[] = [];
productos: productoDTO[];
columnas = ['imagen','nombre','descripcion','inventario','precio','descuento','fechaCreacion','fechaModificacion','categoriaDescripcion','subcategoriaDescripcion' ,'acciones'];
cantidadTotalRegistros;
paginaActual = 1;
cantidadRegistrosMostrados = 12;

ngOnInit(): void {
  this.productosService.obtenerTodasImagenes().subscribe((res)=>{
    console.log(res);
  });
  this.cargarRegistros(this.paginaActual,this.cantidadRegistrosMostrados);
}

cargarRegistros(pagina: number, cantidadElementosAMostrar){
  this.productosService.obtenerProductos(pagina, cantidadElementosAMostrar).pipe(take(1))
  .subscribe({
    next: res => {
      this.productos = res.body;
      this.cantidadTotalRegistros = res.headers.get("cantidadTotalRegistros");
    },
    error: errores =>{
      this.productos = new Array(0);
      this.errores = parsearErroresAPI(errores);
    }
  });
}

actualizarPaginacion(datos: PageEvent){
  this.paginaActual = datos.pageIndex + 1;
  this.cantidadRegistrosMostrados = datos.pageSize;
  this.cargarRegistros(this.paginaActual, this.cantidadRegistrosMostrados);
}

borrar(id:string){
  this.productosService.obtenerListadoImagenesProducto(id).pipe(take(1))
      .subscribe({
        next: res => {
          res.body.forEach(x=> {
            this.productosService.borrarImagen(x).pipe(take(1)).subscribe();
          });
          this.borrarProducto(id);
        },
        error: (error) => {
          this.borrarProducto(id);
        }
      });
}

borrarProducto(id:string){
  this.productosService.borrarProducto(id).pipe(take(1)).subscribe({
    next: (res) => {
      window.location.reload();
  },
  error: (error) => this.errores = parsearErroresAPI(error)}
  );
}

getBase64(image: string){
  return dataURI(image);
}

}
