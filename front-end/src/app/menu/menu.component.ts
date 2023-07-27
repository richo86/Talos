import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { Router } from '@angular/router';
import { SeguridadService } from '../seguridad/seguridad.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {

  @ViewChild('sidenav') sidenav: MatSidenav;

  openMenu: boolean = false;
  userId: string;

  constructor(public securityService: SeguridadService,
    private router: Router) { }

  ngOnInit(): void {
    if(this.userId == null)
      this.securityService.getUserID(this.securityService.obtenerCampoJwt('email'))
        .subscribe(res =>{
          this.userId = res;
        });
  }

  close() {
    this.sidenav.close();
    this.toggleMenu();
  }

  shouldRun = /(^|.)(stackblitz|webcontainer).(io|com)$/.test(window.location.host);

  logOut(){
    this.securityService.logOut();
    this.router.navigate(['/']);
  }

  toggleMenu(){
    this.openMenu = !this.openMenu;
  }

  redirect(location:string){
    this.router.navigate([location]);
    this.close();
  }

}
