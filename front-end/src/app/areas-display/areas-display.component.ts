import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs';
import { CategoriaDTO } from '../categorias/categorias.models';
import { productoDTO } from '../productos/productos.models';
import { SeguridadService } from '../seguridad/seguridad.service';
import { AreasDisplayService } from './areas-display.service';

@Component({
  selector: 'app-areas-display',
  templateUrl: './areas-display.component.html',
  styleUrls: ['./areas-display.component.css']
})
export class AreasDisplayComponent implements OnInit {

  products : productoDTO[];
  categories : CategoriaDTO[];
  countryCode:string;
  areaId:string;

  constructor(private areasDisplayService:AreasDisplayService,
              private securityService:SeguridadService) { }

  ngOnInit(): void {
    this.GetUserLocation();
  }

  GetUserLocation(){
    this.securityService.getUserLocation().pipe(take(1))
    .subscribe({
      next: res =>{
        this.countryCode = res.body.countryCode;

        this.GetCategoryProducts(this.countryCode,this.areaId);
        this.GetCategories(this.countryCode,this.areaId);
      }
    });
  }

  GetCategoryProducts(countryCode:string,id:string){
    this.areasDisplayService.getCategoryProducts(countryCode,id).pipe(take(1))
    .subscribe({
      next: (res) =>{
        this.products = res.body;
      }
    })
  }

  GetCategories(countryCode:string,id:string){
    this.areasDisplayService.getCategories(this.areaId).pipe(take(1))
    .subscribe({
      next: (res) =>{
        this.categories = res.body;
      }
    })
  }

}
