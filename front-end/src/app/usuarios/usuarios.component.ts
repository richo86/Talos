import { HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { usuarioDTO } from '../seguridad/seguridad';
import { MostrarErroresComponent } from '../utilidades/mostrar-errores/mostrar-errores.component';
import { parsearErroresAPI } from '../utilidades/utilidades';
import { UsuariosService } from './usuarios.service';

@Component({
  selector: 'app-usuarios',
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.css']
})
export class UsuariosComponent implements OnInit {

  constructor(private usuariosService : UsuariosService,
              private router: Router) { }

  errores: string[] = [];
  usuarios: usuarioDTO[];
  columnas = ['nombre','correo','telefono','direccion','roles','acciones'];
  cantidadTotalRegistros;
  paginaActual = 1;
  cantidadRegistrosMostrados = 12;

  ngOnInit(): void {
    this.cargarRegistros(this.paginaActual,this.cantidadRegistrosMostrados);
  }

  cargarRegistros(pagina: number, cantidadElementosAMostrar){
    this.usuariosService.obtenerTodos(pagina, cantidadElementosAMostrar)
    .subscribe({
      next: (respuesta: HttpResponse<usuarioDTO[]>) => {
      this.usuarios = respuesta.body;
      this.cantidadTotalRegistros = respuesta.headers.get("cantidadTotalRegistros");
      }, 
      error: errores => {
        this.usuarios = new Array(0);
        this.errores = parsearErroresAPI(errores)
      }
    });
  }

  actualizarPaginacion(datos: PageEvent){
    this.paginaActual = datos.pageIndex + 1;
    this.cantidadRegistrosMostrados = datos.pageSize;
    this.cargarRegistros(this.paginaActual, this.cantidadRegistrosMostrados);
  }

  hacerAdmin(id : string){
    this.usuariosService.hacerAdmin(id)
    .subscribe(
      {
        next: (res) => {
          Swal.fire({
            text: '¡Operación exitosa!',
            icon: 'success'
          });
          this.cargarRegistros(this.paginaActual, this.cantidadRegistrosMostrados)
        },
        error: (error) => this.errores = parsearErroresAPI(error)
      });
  }

  quitarAdmin(id: string){
    this.usuariosService.quitarAdmin(id)
    .subscribe(
      {
        next: (res) => {
          Swal.fire({
            text: '¡Operación exitosa!',
            icon: 'success'
          });
          this.cargarRegistros(this.paginaActual, this.cantidadRegistrosMostrados)
        },
        error: (error) => this.errores = parsearErroresAPI(error)
      });
  }

  borrar(id: string){
    this.usuariosService.borrarUsuario(id)
    .subscribe({
      next: (res) => {
        this.cargarRegistros(this.paginaActual,this.cantidadRegistrosMostrados);
        Swal.fire({
          text: '¡Operación exitosa!',
          icon: 'success'
        });
      },
      error: (errores) => this.errores = parsearErroresAPI(errores)
    });
  }

}
