import { Component, NgZone, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CredentialResponse } from 'google-one-tap';
import { SeguridadService } from '../seguridad/seguridad.service';
import { environment } from 'src/environments/environment';
import { respuestaAutenticacion } from '../seguridad/seguridad';

@Component({
  selector: 'app-social-login',
  templateUrl: './social-login.component.html',
  styleUrls: ['./social-login.component.css']
})
export class SocialLoginComponent implements OnInit {

  private clientId = environment.clientIdG;
  private respuestaAutenticacion : respuestaAutenticacion = {
    token: '',
    expiracion: undefined
  };

  constructor(private router: Router,
              private seguridadService: SeguridadService,
              private ngZone: NgZone) { }

  ngOnInit(): void {
    // @ts-ignore
    google.accounts.id.initialize({
      client_id: this.clientId,
      callback: this.handleCredentialResponse.bind(this),
      auto_select: false,
      cancel_on_tap_outside: true
    });

    // @ts-ignore
    const parent = document.getElementById('google_btn');
    // @ts-ignore
    google.accounts.id.renderButton(parent, {theme: "outlined", size: "large", width: 350});
    // @ts-ignore
    google.accounts.id.prompt();
  }

  async handleCredentialResponse(response: CredentialResponse) {
    await this.seguridadService.loginWithGoogle(response.credential).subscribe({
      next: (res) => {
        this.respuestaAutenticacion.expiracion = res.expiracion;
        this.respuestaAutenticacion.token = res.token;
        this.seguridadService.guardarToken(this.respuestaAutenticacion);
        location.reload();
      },
      error: (error:any) => {
        console.log(error);
      }
    });
  }

}
