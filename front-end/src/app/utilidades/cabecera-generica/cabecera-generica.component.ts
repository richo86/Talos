import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-cabecera-generica',
  templateUrl: './cabecera-generica.component.html',
  styleUrls: ['./cabecera-generica.component.css']
})
export class CabeceraGenericaComponent implements OnInit {

  @Input()
  titulo: string;

  constructor() { }

  ngOnInit(): void {
  }

}
