import { HttpResponse } from '@angular/common/http';
import { Component, OnInit, Inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { parsearErroresAPI } from '../utilidades/utilidades';
import { DetalleVentaComponent } from './detalle-venta/detalle-venta.component';
import { ItemsPedidoDTO, ventasDTO } from './ventas.models';
import { VentasService } from './ventas.service';

@Component({
  selector: 'app-ventas',
  templateUrl: './ventas.component.html',
  styleUrls: ['./ventas.component.css']
})
export class VentasComponent implements OnInit {

  constructor(private ventasService : VentasService,
    private router: Router,
    public dialog: MatDialog) { }

  errores: string[] = [];
  ventas: ventasDTO[];
  columnas = ['nombreUsuario','totalVenta','totalVentaSinIVA','metodoPago','estado','observaciones','tipoVenta','fechaActualizacion','acciones'];
  cantidadTotalRegistros;
  paginaActual = 1;
  cantidadRegistrosMostrados = 12;

  ngOnInit(): void {
    this.cargarRegistros(this.paginaActual,this.cantidadRegistrosMostrados);
  }

  cargarRegistros(pagina: number, cantidadElementosAMostrar){
    this.ventasService.obtenerTodos(pagina, cantidadElementosAMostrar)
    .subscribe({
      next: (respuesta: HttpResponse<ventasDTO[]>) => {
        this.ventas = respuesta.body;
        this.cantidadTotalRegistros = respuesta.headers.get("cantidadTotalRegistros");
      },
      error: errores =>{
        this.errores = parsearErroresAPI(errores);
      }
    });
  }

  actualizarPaginacion(datos: PageEvent){
    this.paginaActual = datos.pageIndex + 1;
    this.cantidadRegistrosMostrados = datos.pageSize;
    this.cargarRegistros(this.paginaActual, this.cantidadRegistrosMostrados);
  }

  openDialog(id: string) {
    const venta = this.ventas.find(x=>x.id == id);

    this.dialog.open(DetalleVentaComponent, {
      data : venta.itemsPedido
    });
  }

}
