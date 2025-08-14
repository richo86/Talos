import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import * as DiscountsActions from './discountsActions';
import { catchError, map, of, switchMap } from "rxjs";
import { LandingService } from "src/app/landing-page/landing.service";

@Injectable()
export class DiscountsEffects{
    GetDiscountedProducts$ = createEffect(() => 
        this.actions$.pipe(
            ofType(DiscountsActions.getDiscounts),
            switchMap((action: any) => {
                return this.landingService.GetDiscountedProducts(action.countryCode)
                    .pipe(
                        map((products) => DiscountsActions.getDiscountsSuccess({ ProductsDTO: products.body })),
                        catchError((errors) => of(DiscountsActions.getDiscountsFailure({error: errors.message})))
                    );
            })
        )
    );

    constructor(private actions$: Actions, private landingService:LandingService){}
}