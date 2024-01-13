import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { filter, take } from 'rxjs';
import { parsearErroresAPI } from 'src/app/utilidades/utilidades';
import { credencialesUsuario, usuarioDTO } from '../seguridad';
import { SeguridadService } from '../seguridad.service'
import Swal from 'sweetalert2';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css']
})
export class RegistroComponent implements OnInit {

  constructor(private seguridadService: SeguridadService,
    private router: Router) { }

  errores: string[] = [];
  currentRoute:string;
  showSocials:boolean = false;

  ngOnInit(): void {
  }

  registrar(usuario: usuarioDTO){
    this.errores = [];
    this.seguridadService.registrar(usuario).pipe(take(1))
      .subscribe({
        next: (res) => {
          this.seguridadService.guardarToken(res);
          this.router.navigate(['/']);
        },
        error: errores => {
          this.errores = parsearErroresAPI(errores);
          Swal.fire({
            text: 'An error occurred while creating the user',
            icon: 'error'
          });
        }        
      });
  }

  getRoute(){
    this.router.events
    .pipe(
      filter(e => e instanceof NavigationEnd)
    )
    .subscribe( (navEnd:NavigationEnd) => {
      this.currentRoute = navEnd.urlAfterRedirects;
      this.verifyPage();
    });
  }

  verifyPage(){
    if(this.currentRoute === '/')
      this.showSocials = true;
    else
      this.showSocials = false;
  }

}
