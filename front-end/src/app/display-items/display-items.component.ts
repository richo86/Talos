import { Component, Input, OnInit } from '@angular/core';
import { CategoriaDTO } from '../categorias/categorias.models';
import { productoDTO } from '../productos/productos.models';

@Component({
  selector: 'app-display-items',
  templateUrl: './display-items.component.html',
  styleUrls: ['./display-items.component.css']
})
export class DisplayItemsComponent implements OnInit {
  @Input()
  products : productoDTO[];
  @Input()
  categories : CategoriaDTO[];

  constructor() { }

  ngOnInit(): void {
  }

}
