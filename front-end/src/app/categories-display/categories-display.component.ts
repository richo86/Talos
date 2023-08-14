import { Component, OnInit } from '@angular/core';
import { productoDTO } from '../productos/productos.models';

@Component({
  selector: 'app-categories-display',
  templateUrl: './categories-display.component.html',
  styleUrls: ['./categories-display.component.css']
})
export class CategoriesDisplayComponent implements OnInit {

  categories : productoDTO[];

  constructor() { }

  ngOnInit(): void {
    this.GetCategoryProducts();
    this.GetSubcategories();
  }

  GetCategoryProducts(){

  }

  GetSubcategories(){

  }

}
