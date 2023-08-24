import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { take } from 'rxjs';
import { passwordMatchingValidator } from 'src/app/utilidades/validadores/confirmarContraseña';
import { credencialesUsuario, usuarioDTO } from '../seguridad';
import { SeguridadService } from '../seguridad.service';
import { generoDTO } from './registro';

@Component({
  selector: 'app-formulario-registro',
  templateUrl: './formulario-registro.component.html',
  styleUrls: ['./formulario-registro.component.css']
})
export class FormularioRegistroComponent implements OnInit {

  constructor(private formBuilder: FormBuilder,
              private seguridadService: SeguridadService) { }

  @Input()
  errores: string[] = [];

  @Input()
  accion: string;

  @Output()
  onSubmit: EventEmitter<usuarioDTO> = new EventEmitter<usuarioDTO>();

  form: FormGroup;
  generos: generoDTO[] = [];

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      email: ['',{validators: [Validators.required, Validators.email]}],
      password: ['',{validators: [Validators.required]}],
      confirmPassword:['',{validators:[Validators.required]}],
      firstName: ['',{validators:[]}],
      middleName: ['',{validators:[]}],
      firstLastName: ['',{validators:[]}],
      secondLastName: ['',{validators:[]}],
      address: ['',{validators:[]}],
      country: ['',{validators:[]}],
      gender: ['',{validators:[]}],
      phoneNumber:['',{validators:[]}]
    },{validators: passwordMatchingValidator});

    this.seguridadService.obtenerGeneros().pipe(take(1))
    .subscribe(res => {
      this.generos = res.body;
      console.log(this.generos);
    });
  }

  ObtenerMensajeErrorEmail(){
     var campo = this.form.get('email');
     if(campo.hasError('required'))
        return "El campo email es requerido";
     else if(campo.hasError('email'))
        return "El email no es válido";
  }

}
