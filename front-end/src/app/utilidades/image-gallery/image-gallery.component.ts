import { Component, Input, OnInit } from '@angular/core';
import { productoDTO } from 'src/app/productos/productos.models';
import { dataURI } from '../utilidades';

@Component({
  selector: 'app-image-gallery',
  templateUrl: './image-gallery.component.html',
  styleUrls: ['./image-gallery.component.css']
})
export class ImageGalleryComponent implements OnInit {

  @Input()
  product: productoDTO;

  productName: string;
  activeSrc: string;

  constructor() { }

  ngOnInit(): void {
    this.productName = this.product.nombre;
    this.activeSrc = this.base64(this.product.imagen[0]);
  }

  base64(data:string){
    return dataURI(data);
  }

  onImageClicked(index:number){
    this.activeSrc = this.base64(this.product.imagen[index]);
  }

}
