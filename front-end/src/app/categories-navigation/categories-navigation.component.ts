import { AfterViewInit, Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { CategoriasProductoDTO } from '../menu/menu.models';
import { MenuService } from '../menu/menu.service';
import { filter, take } from 'rxjs';
import { Location } from '@angular/common';

@Component({
  selector: 'app-categories-navigation',
  templateUrl: './categories-navigation.component.html',
  styleUrls: ['./categories-navigation.component.css']
})
export class CategoriesNavigationComponent implements OnInit {

  showNavigation:boolean = false;
  productMenu: CategoriasProductoDTO = new CategoriasProductoDTO();
  currentRoute: string;

  constructor(private router:Router,
              private menuService:MenuService,
              private location:Location) { }

  ngOnInit(): void {
    this.getRoute();
    this.getProductMenu();
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
      this.showNavigation = true;
    else
      this.showNavigation = false;
  }

  getProductMenu(){
    this.menuService.obtenerMenuProductos().pipe(take(1))
    .subscribe({
      next: res =>{
        if(!!res)
          this.productMenu = res.body;
      }
    });
  }

}
