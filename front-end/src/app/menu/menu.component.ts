import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { Router } from '@angular/router';
import { SeguridadService } from '../seguridad/seguridad.service';
import { MenuService } from './menu.service';
import { AreasDTO, CategoriasProductoDTO } from './menu.models';

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


  constructor(public securityService: SeguridadService,
    private router: Router,
    private menuService: MenuService) { }

  ngOnInit(): void {
    this.getUserId();
    this.getProductMenu();
  }

  getUserId(){
    if(this.userId == null)
    this.securityService.getUserID(this.securityService.obtenerCampoJwt('email'))
    .subscribe(res =>{
      this.userId = res;
    });
  }

  getProductMenu(){
    this.menuService.obtenerMenuProductos()
    .subscribe({
      next: res =>{
        if(!!res)
          this.productMenu = res.body;
      }
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

}
