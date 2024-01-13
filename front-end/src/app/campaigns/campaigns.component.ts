import { Component, OnInit } from '@angular/core';
import { campaignDTO } from './campaigns.models';
import { CampaignsService } from './campaigns.service';
import { parsearErroresAPI } from '../utilidades/utilidades';
import { PageEvent } from '@angular/material/paginator';
import { take } from 'rxjs';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-campaigns',
  templateUrl: './campaigns.component.html',
  styleUrls: ['./campaigns.component.css']
})
export class CampaignsComponent implements OnInit {

  errores: string[] = [];
  campaigns: campaignDTO[];
  columnas = ['nombre','descripcion','estado','porcentajeDescuento','fechaCreacion','fechaEdicion','acciones'];
  cantidadTotalRegistros;
  paginaActual = 1;
  cantidadRegistrosMostrados = 12;

  constructor(private campaignService:CampaignsService) { }

  ngOnInit(): void {
    this.cargarRegistros(this.paginaActual,this.cantidadRegistrosMostrados);
  }

  cargarRegistros(pagina: number, cantidadElementosAMostrar){
    this.campaignService.obtenerCampañas(pagina, cantidadElementosAMostrar).pipe(take(1))
    .subscribe({
      next: res => {
        this.campaigns = res.body;
        this.cantidadTotalRegistros = res.headers.get("cantidadTotalRegistros");
      },
      error: errores =>{
        this.errores = parsearErroresAPI(errores);
        this.campaigns = new Array(0);
      }
    })
  }
  
  actualizarPaginacion(datos: PageEvent){
    this.paginaActual = datos.pageIndex + 1;
    this.cantidadRegistrosMostrados = datos.pageSize;
    this.cargarRegistros(this.paginaActual, this.cantidadRegistrosMostrados);
  }
  
  borrar(id:string){
    this.campaignService.borrarCampaña(id).pipe(take(1))
    .subscribe({
      next: (res) => {
      Swal.fire({
        text: '¡Operación exitosa!',
        icon: 'success'
      });
    },
    error: (error) => this.errores = parsearErroresAPI(error)}
    );
  }

}
