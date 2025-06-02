import { Component, OnInit } from '@angular/core';
import { SeguridadService } from '../seguridad/seguridad.service';
import { Store } from '@ngrx/store';
import { getCart } from '../utilidades/Redux/Cart/cartActions';
@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  constructor(private securityService: SeguridadService,
              private store:Store) { }

  ngOnInit(): void {
    this.store.dispatch(getCart({id:"1"}));
  }

}
