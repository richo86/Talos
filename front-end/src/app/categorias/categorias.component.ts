import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { parsearErroresAPI } from '../utilidades/utilidades';
import { CategoriaDTO } from './categorias.models';
import { CategoriasService } from './categorias.service';

@Component({
  selector: 'app-categorias',
  templateUrl: './categorias.component.html',
  styleUrls: ['./categorias.component.css']
})
export class CategoriasComponent implements OnInit {

  constructor(private categoriasService: CategoriasService,
    private router: Router) { }

  errores: string[] = [];
  areas: CategoriaDTO[];
  categorias: CategoriaDTO[];
  subcategorias: CategoriaDTO[];
  columnas = ['descripcion','codigo','acciones'];
  columnasSub = ['descripcion','codigo','categoriaPrincipal','acciones'];
  cantidadTotalRegistrosPrincipal;
  cantidadTotalRegistrosSecundaria;
  cantidadTotalRegistrosArea;
  paginaActual = 1;
  cantidadRegistrosMostrados = 12;
  paginaActualSub = 1;
  cantidadRegistrosMostradosSub = 12;
  paginaActualAreas = 1;
  cantidadRegistrosMostradosAreas = 12;

  ngOnInit(): void {
    this.cargarRegistros(this.paginaActual,this.cantidadRegistrosMostrados);
    this.cargarSubcategorias(this.paginaActualSub,this.cantidadRegistrosMostradosSub);
    this.cargarAreas(this.paginaActualAreas,this.cantidadRegistrosMostradosAreas);
  }
  
  cargarRegistros(pagina: number, cantidadElementosAMostrar){
    this.categoriasService.obtenerCategorias(pagina, cantidadElementosAMostrar)
    .subscribe({
      next: res => {
        this.categorias = res.body;
        this.cantidadTotalRegistrosPrincipal = res.headers.get("cantidadTotalRegistros");
      },
      error: errores =>{
        this.categorias = new Array(0);
        this.errores = parsearErroresAPI(errores);
      }
    });
  }

  cargarAreas(pagina: number, cantidadElementosAMostrar){
    this.categoriasService.obtenerAreas(pagina, cantidadElementosAMostrar)
    .subscribe({
      next: res => {
        this.areas = res.body;
        this.cantidadTotalRegistrosArea = res.headers.get("cantidadTotalRegistros");
      },
      error: errores =>{
        this.areas = new Array(0);
        this.errores = parsearErroresAPI(errores);
      }
    });
  }

  cargarSubcategorias(pagina: number, cantidadElementosAMostrar){
    this.categoriasService.obtenerSubcategorias(pagina, cantidadElementosAMostrar)
    .subscribe({
      next: res => {
        this.subcategorias = res.body;
        this.cantidadTotalRegistrosSecundaria = res.headers.get("cantidadTotalRegistros");
      },
      error: errores =>{
        this.subcategorias = new Array(0);
        this.errores = parsearErroresAPI(errores);
      }
    });
  }
  
  actualizarPaginacion(datos: PageEvent){
    this.paginaActual = datos.pageIndex + 1;
    this.cantidadRegistrosMostrados = datos.pageSize;
    this.cargarRegistros(this.paginaActual, this.cantidadRegistrosMostrados);
  }

  actualizarPaginacionSub(datos: PageEvent){
    this.paginaActualSub = datos.pageIndex + 1;
    this.cantidadRegistrosMostradosSub = datos.pageSize;
    this.cargarSubcategorias(this.paginaActualSub, this.cantidadRegistrosMostradosSub);
  }

  actualizarPaginacionAreas(datos: PageEvent){
    this.paginaActualAreas = datos.pageIndex + 1;
    this.cantidadRegistrosMostradosAreas = datos.pageSize;
    this.cargarAreas(this.paginaActualAreas, this.cantidadRegistrosMostradosAreas);
  }

  borrar(id:string){
    this.categoriasService.borrarCategoria(id).subscribe({
      next: (res) => {
      Swal.fire({
        text: '¡Operación exitosa!',
        icon: 'success'
      }).then(res => {
        window.location.reload();
      });
    },
    error: (error) => this.errores = parsearErroresAPI(error)}
    );
  }

}