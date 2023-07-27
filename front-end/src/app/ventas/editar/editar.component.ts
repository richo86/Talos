import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { parsearErroresAPI } from 'src/app/utilidades/utilidades';
import Swal from 'sweetalert2';
import { PaymentType, ventasDTO } from '../ventas.models';
import { VentasService } from '../ventas.service';

@Component({
  selector: 'app-editar-venta',
  templateUrl: './editar.component.html',
  styleUrls: ['./editar.component.css']
})
export class EditarVentaComponent implements OnInit {

  constructor(private formBuilder: FormBuilder,
    private ventasService: VentasService,
    private activatedRoute: ActivatedRoute) { }

  errores: string[] = [];
  accion: string;
  form: FormGroup;
  venta: ventasDTO;
  tiposVenta: PaymentType[];
  idVenta: string;

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      nombreUsuario: ['',{validators: [Validators.required]}],
      totalVenta: ['',{validators: []}],
      totalVentaSinIVA:['',{validators:[]}],
      metodoPago: ['',{validators:[]}],
      estado: ['',{validators:[]}],
      observaciones: ['',{validators:[]}],
      tipoVenta: ['',{validators:[]}]
    });

    this.ventasService.GetPaymentTypes().subscribe({
      next: res => {
        this.tiposVenta = res.body;
      }
    });

    this.activatedRoute.params.subscribe((params) => {
      this.idVenta = params.id;
      this.ventasService.GetSale(params.id)
      .subscribe({
        next: res => {
          this.venta = res.body;
          this.form.patchValue(this.venta);
        }
      });
    });

  }

  editar(){
    this.venta = this.form.value;
    this.venta.id = this.idVenta;
    this.ventasService.Editar(this.venta)
      .subscribe({
        next: res => {
          Swal.fire({
            text: '¡Operación exitosa!',
            icon: 'success'
          })
        },
        error: (errores) => this.errores = parsearErroresAPI(errores)
      });
  }

}
