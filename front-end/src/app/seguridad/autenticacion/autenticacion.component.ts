import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { credencialesUsuario } from '../seguridad';

@Component({
  selector: 'app-autenticacion',
  templateUrl: './autenticacion.component.html',
  styleUrls: ['./autenticacion.component.css']
})
export class AutenticacionComponent implements OnInit {

  constructor(private formBuilder: FormBuilder) { }

  @Input()
  errores: string[] = [];

  @Input()
  accion: string;

  @Output()
  onSubmit: EventEmitter<credencialesUsuario> = new EventEmitter<credencialesUsuario>();

  form: FormGroup;

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      email: ['',{validators: [Validators.required, Validators.email]}],
      password: ['',{validators: [Validators.required]}]
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
