import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { take } from 'rxjs';
import { credencialesUsuario } from '../seguridad/seguridad';
import { SeguridadService } from '../seguridad/seguridad.service';
import { parsearErroresAPI } from '../utilidades/utilidades';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private securityService : SeguridadService,
    private router: Router) { }

  errores: string[] = [];

  ngOnInit(): void {
  }

  Ingresar(credenciales : credencialesUsuario){
    this.securityService.login(credenciales).pipe(take(1))
    .subscribe(
      {
        next: (res) => {
          this.securityService.guardarToken(res);
          this.router.navigate(['/']);
        },
        error: (errores) => {
          this.errores = parsearErroresAPI(errores);
          Swal.fire({
            text: 'An error occurred while trying to log in, please try again later',
            icon: 'error'
          });
        }
      });
  }

}
