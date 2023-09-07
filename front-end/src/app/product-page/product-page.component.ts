import { Component, Input, OnInit } from '@angular/core';
import { take } from 'rxjs';
import { productoDTO } from '../productos/productos.models';
import { ProductosService } from '../productos/productos.service';

@Component({
  selector: 'app-product-page',
  templateUrl: './product-page.component.html',
  styleUrls: ['./product-page.component.css']
})
export class ProductPageComponent implements OnInit {

  @Input()
  id:string;

  product:productoDTO;

  constructor(private productService: ProductosService) { }

  ngOnInit(): void {
    this.GetProduct(this.id);
  }

  GetProduct(id:string){
    this.productService.obtenerProducto(id).pipe(take(1))
    .subscribe((res) => {
      this.product = res;
    });
  }

}
