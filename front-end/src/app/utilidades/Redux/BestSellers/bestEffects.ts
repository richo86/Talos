import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import * as BestActions from './bestActions';
import { catchError, map, of, switchMap } from "rxjs";
import { LandingService } from "src/app/landing-page/landing.service";

@Injectable()
export class BestSellersEffects{
    getBestSellers$ = createEffect(() => 
        this.actions$.pipe(
            ofType(BestActions.getBestSellers),
            switchMap((action: any) => {
                return this.landingService.GetBestSellers(action.countryCode)
                    .pipe(
                        map((products) => BestActions.getBestSellersSuccess({ ProductsDTO: products.body })),
                        catchError((errors) => of(BestActions.getGetBestSellersFailure({error: errors.message})))
                    );
            })
        )
    );

    constructor(private actions$: Actions, private landingService:LandingService){}
}