import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { take } from 'rxjs';
import { parsearErroresAPI } from 'src/app/utilidades/utilidades';
import Swal from 'sweetalert2';
import { descuentoDTO } from '../descuentos.models';
import { DescuentosService } from '../descuentos.service';

@Component({
  selector: 'app-ced',
  templateUrl: './ced.component.html',
  styleUrls: ['./ced.component.css']
})
export class CEDComponent implements OnInit {

  constructor(private formBuilder: FormBuilder,
              private activatedRoute: ActivatedRoute,
              private descuentosService: DescuentosService,
              private router: Router) { }

  form: FormGroup;
  errores: string[] = [];
  descuentos: descuentoDTO = {
    id: null,
    nombre: null,
    descripcion: null,
    estado: null,
    porcentajeDescuento: null,
    fechaCreacion: new Date(),
    fechaEdicion: new Date()
  };

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      nombre: ['',{validators: [Validators.required]}],
      descripcion: ['',{validators: [Validators.required]}],
      estado:['',{validators:[Validators.required]}],
      porcentajeDescuento: ['',{validators:[Validators.required]}]
    });

    this.activatedRoute.params.subscribe((params) => {
      if(!!params.id){
        this.descuentosService.obtenerDescuento(params.id).pipe(take(1))
        .subscribe({
          next: res => {
            this.descuentos = res.body;
            this.form.patchValue(this.descuentos);
          }
        });
      }
    });
  }

  create(){
    this.activatedRoute.params.subscribe((params) => {
      this.descuentos = Object.assign(this.descuentos,this.form.value);
      if(!!params.id){
        this.descuentosService.actualizarDescuento(this.descuentos).pipe(take(1))
        .subscribe({
          next: (res) => {
            Swal.fire({
              text: '¡Operación exitosa!',
              icon: 'success'
            }).then(res =>{
              if(res.isConfirmed)
                this.router.navigate(['/descuentos']);
            });
          },
          error: (error) => this.errores = parsearErroresAPI(error)
        });
      }else{
        this.descuentosService.crearDescuento(this.descuentos).pipe(take(1))
        .subscribe({
          next: (res) => {
            Swal.fire({
              text: '¡Operación exitosa!',
              icon: 'success'
            }).then(res =>{
              if(res.isConfirmed)
                this.router.navigate(['/descuentos']);
            });
          },
          error: (error) => this.errores = parsearErroresAPI(error)
        });
      }
    });
  }

}
