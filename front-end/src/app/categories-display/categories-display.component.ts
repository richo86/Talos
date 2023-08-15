import { Component, Input, OnInit } from '@angular/core';
import { CategoriaDTO } from '../categorias/categorias.models';
import { productoDTO } from '../productos/productos.models';
import { SeguridadService } from '../seguridad/seguridad.service';
import { CategoriesDisplayService } from './categories-display.service';

@Component({
  selector: 'app-categories-display',
  templateUrl: './categories-display.component.html',
  styleUrls: ['./categories-display.component.css']
})
export class CategoriesDisplayComponent implements OnInit {

  @Input()
  categoryId:string;

  products : productoDTO[];
  categories : CategoriaDTO[];
  countryCode:string;

  constructor(private categoryDisplayService:CategoriesDisplayService,
    private securityService:SeguridadService) { }

  ngOnInit(): void {
    this.GetUserLocation();
  }

  GetUserLocation(){
    this.securityService.getUserLocation()
    .subscribe({
      next: res =>{
        this.countryCode = res.body.countryCode;

        this.GetCategoryProducts(this.countryCode,this.categoryId);
        this.GetSubcategories(this.countryCode,this.categoryId);
      }
    });
  }

  GetCategoryProducts(countryCode:string,id:string){
    this.categoryDisplayService.getCategoryProducts(countryCode,id)
    .subscribe({
      next: (res) =>{
        this.products = res.body;
      }
    })
  }

  GetSubcategories(countryCode:string,id:string){
    this.categoryDisplayService.getSubcategories(this.categoryId)
    .subscribe({
      next: (res) =>{
        this.categories = res.body;
      }
    })
  }

}
