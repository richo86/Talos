import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { take } from 'rxjs';
import { CartDTO } from 'src/app/cart/cartDTO.models';
import { productoDTO } from 'src/app/productos/productos.models';
import { SeguridadService } from 'src/app/seguridad/seguridad.service';
import Swal from 'sweetalert2';
import { GUID } from '../guid';
import { dataURI } from '../utilidades';
import { ProductCardService } from './product-card.service';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent implements OnInit {

  @Input() 
  productDTO: productoDTO;

  quantity: number = 1;

  constructor(private router: Router,
    private securityService: SeguridadService,
    private productCardService: ProductCardService) { }

  ngOnInit(): void {
    
  }

  AddToCart(productId:string){
    let cart : CartDTO = {
      id : new GUID().toString(),
      sesionId : null,
      productoId : productId,
      cantidad : this.quantity,
      fechaCreacion : new Date(),
      fechaModificacion : new Date(),
      email : this.securityService.getUserId(),
    }

    this.productCardService.CreateCart(cart).pipe(take(1))
    .subscribe(()=>{
      Swal.fire({
        text: '¡Item added!',
        icon: 'success'
      })
    });
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

  base64(data:string){
    return dataURI(data);
  }

}
