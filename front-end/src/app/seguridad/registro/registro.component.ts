import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { take } from 'rxjs';
import { parsearErroresAPI } from 'src/app/utilidades/utilidades';
import { credencialesUsuario, usuarioDTO } from '../seguridad';
import { SeguridadService } from '../seguridad.service'

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css']
})
export class RegistroComponent implements OnInit {

  constructor(private seguridadService: SeguridadService,
    private router: Router) { }

  errores: string[] = [];

  ngOnInit(): void {
  }

  registrar(usuario: usuarioDTO){
    this.errores = [];
    this.seguridadService.registrar(usuario).pipe(take(1))
      .subscribe((res) => {
        this.seguridadService.guardarToken(res);
        this.router.navigate(['/']);
      }, errores => this.errores = parsearErroresAPI(errores));
  }

}
