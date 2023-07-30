import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { productoDTO } from 'src/app/productos/productos.models';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent implements OnInit {

  @Input() 
  productDTO: productoDTO;

  quantity: number = 1;

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  AddToCart(){
  }

  redirect(location:string){
    this.router.navigate([location]);
  }

  incrementQty(){
    if(this.quantity != 100)
      this.quantity += 1;
  }

  decrementQty(){
    if(this.quantity != 1)
      this.quantity -= 1;
  }

  onQtyChange(event){
    let value = event.target.value;
    if(value < 1)
      this.quantity = 1;

    if(value > 100)
      this.quantity = 100;
  }

}
