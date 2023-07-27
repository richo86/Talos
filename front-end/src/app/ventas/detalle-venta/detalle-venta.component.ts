import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ItemsPedidoDTO } from '../ventas.models';

@Component({
  selector: 'app-detalle-venta',
  templateUrl: './detalle-venta.component.html',
  styleUrls: ['./detalle-venta.component.css']
})
export class DetalleVentaComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: ItemsPedidoDTO[]) { }

  ngOnInit(): void {
  }

}
