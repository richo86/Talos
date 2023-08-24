import { Component, Input, OnInit } from '@angular/core';
import { CategoriaDTO } from '../categorias/categorias.models';

@Component({
  selector: 'app-area-card',
  templateUrl: './area-card.component.html',
  styleUrls: ['./area-card.component.css']
})
export class AreaCardComponent implements OnInit {

  @Input()
  category: CategoriaDTO;

  constructor() { }

  ngOnInit(): void {
  }

}
