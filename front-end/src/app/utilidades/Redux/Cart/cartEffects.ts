import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { CartService } from "src/app/cart/cart.service";
import * as StoreActions from './cartActions';
import { catchError, map, of, switchMap } from "rxjs";

@Injectable()
export class CartEffects{
    getCart$ = createEffect(() => 
        this.actions$.pipe(
            ofType(StoreActions.getCart),
            switchMap((action: any) => {
                return this.cartService.getCart(action.id)
                    .pipe(
                        map((cart) => StoreActions.getCartSuccess({ CartDTO: cart.body })),
                        catchError((errors) => of(StoreActions.getCartFailure({error: errors.message})))
                    );
            })
        )
    );

    constructor(private actions$: Actions, private cartService:CartService){}
}