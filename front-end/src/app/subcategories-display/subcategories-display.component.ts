import { Component, OnInit } from '@angular/core';
import { productoDTO } from '../productos/productos.models';

@Component({
  selector: 'app-subcategories-display',
  templateUrl: './subcategories-display.component.html',
  styleUrls: ['./subcategories-display.component.css']
})
export class SubcategoriesDisplayComponent implements OnInit {

  products: productoDTO[];

  constructor() { }

  ngOnInit(): void {
    this.GetProductsFromSubcategory();
  }

  GetProductsFromSubcategory(){

  }

}
