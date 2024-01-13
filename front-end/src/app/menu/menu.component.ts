import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { Router } from '@angular/router';
import { SeguridadService } from '../seguridad/seguridad.service';
import { MenuService } from './menu.service';
import { AreasDTO, CategoriasProductoDTO } from './menu.models';
import { take } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { LoginComponent } from '../login/login.component';
import { RegistroComponent } from '../seguridad/registro/registro.component';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {

  @ViewChild('sidenav') sidenav: MatSidenav;

  openMenu: boolean = false;
  userId: string;
  productMenu: CategoriasProductoDTO = new CategoriasProductoDTO();
  shouldRun = /(^|.)(stackblitz|webcontainer).(io|com)$/.test(window.location.host);
  loggedIn: boolean = false;

  constructor(public securityService: SeguridadService,
    private router: Router,
    private menuService: MenuService,
    private dialog: MatDialog) { }

  ngOnInit(): void {
    this.getUserId();
  }

  getUserId(){
    if(this.userId == null)
    this.securityService.getUserID(this.securityService.obtenerCampoJwt('email')).pipe(take(1))
    .subscribe(res =>{
      this.userId = res;
    });
  }

  close() {
    this.sidenav.close();
    this.toggleMenu();
  }

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

  openDialog() {
    const dialogRef = this.dialog.open(LoginComponent);

    dialogRef.afterClosed().subscribe(result => {
      if(result)
        this.openRegisterDialog();
    });
  }

  openRegisterDialog(){
    const registerDialog = this.dialog.open(RegistroComponent);

    registerDialog.afterClosed().subscribe(result => {
      if(result)
        this.openDialog();
    });
  }

}
