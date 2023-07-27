import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { credencialesUsuario, usuarioDTO } from 'src/app/seguridad/seguridad';
import { SeguridadService } from 'src/app/seguridad/seguridad.service';
import { parsearErroresAPI } from 'src/app/utilidades/utilidades';

@Component({
  selector: 'app-crear',
  templateUrl: './crear.component.html',
  styleUrls: ['./crear.component.css']
})
export class CrearComponent implements OnInit {

  constructor(private seguridadService: SeguridadService,
    private router: Router) { }

  errores: string[] = [];

  ngOnInit(): void {
  }

  registrar(usuario: usuarioDTO){
    this.errores = [];
    this.seguridadService.registrar(usuario)
      .subscribe(res => {
        this.router.navigate(['/usuarios']);
      }, errores => this.errores = parsearErroresAPI(errores));
  }

}
