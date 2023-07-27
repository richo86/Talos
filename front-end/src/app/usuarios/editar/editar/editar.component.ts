import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { generoDTO, paisDTO } from 'src/app/seguridad/formulario-registro/registro';
import { usuarioDTO } from 'src/app/seguridad/seguridad';
import { SeguridadService } from 'src/app/seguridad/seguridad.service';
import { parsearErroresAPI } from 'src/app/utilidades/utilidades';
import { passwordMatchingValidator } from 'src/app/utilidades/validadores/confirmarContraseña';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-editar',
  templateUrl: './editar.component.html',
  styleUrls: ['./editar.component.css']
})
export class EditarComponent implements OnInit {

  constructor(private formBuilder: FormBuilder,
    private seguridadService: SeguridadService,
    private activatedRoute: ActivatedRoute) { }

  errores: string[] = [];
  accion: string;
  form: FormGroup;
  generos: generoDTO[] = [];
  usuario: usuarioDTO;
  paises: paisDTO[] = [];
  mostrarEmail: boolean = true;

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      email: ['',{validators: [Validators.required, Validators.email]}],
      password: ['',{validators: []}],
      confirmPassword:['',{validators:[]}],
      firstName: ['',{validators:[]}],
      middleName: ['',{validators:[]}],
      firstLastName: ['',{validators:[]}],
      secondLastName: ['',{validators:[]}],
      address: ['',{validators:[]}],
      country: ['',{validators:[]}],
      gender: ['',{validators:[]}],
      phoneNumber:['',{validators:[]}]
    },{validators: passwordMatchingValidator});

    this.seguridadService.obtenerGeneros()
    .subscribe(res => {
      this.generos = res.body;
    });

    this.seguridadService.obtenerPaises()
    .subscribe(res =>{
      this.paises = res.body;
    })

    this.activatedRoute.params.subscribe((params) => {
      this.seguridadService.getUser(params.id)
      .subscribe({
        next: res => {
          this.usuario = res;
          this.form.patchValue(this.usuario);
          if(this.usuario)
            this.mostrarEmail = false;
        }
      });
    });
  }

  ObtenerMensajeErrorEmail(){
     var campo = this.form.get('email');
     if(campo.hasError('required'))
        return "El campo email es requerido";
     else if(campo.hasError('email'))
        return "El email no es válido";
  }

  editar(){
    const userId = this.usuario.id;
    this.usuario = this.form.value;
    this.usuario.id = userId;

    if(this.usuario.password == null)
      this.usuario.password = "abc";

    this.seguridadService.editar(this.usuario)
      .subscribe({
        next: res => {
          this.usuario = res;
          this.form.patchValue(this.usuario);
          this.form.controls['confirmPassword'].setValue('');
          Swal.fire({
            text: '¡Operación exitosa!',
            icon: 'success'
          })
        },
        error: (errores) => this.errores = parsearErroresAPI(errores)
      });
  }
}
