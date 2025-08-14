import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import * as LowestCostActions from './lowestCostActions';
import { catchError, map, of, switchMap } from "rxjs";
import { LandingService } from "src/app/landing-page/landing.service";

@Injectable()
export class LowestCostEffects{
    GetLowestCost$ = createEffect(() => 
        this.actions$.pipe(
            ofType(LowestCostActions.getLowestCost),
            switchMap((action: any) => {
                return this.landingService.GetLowestCost(action.countryCode)
                    .pipe(
                        map((products) => LowestCostActions.getLowestCostSuccess({ ProductsDTO: products.body })),
                        catchError((errors) => of(LowestCostActions.getLowestCostFailure({error: errors.message})))
                    );
            })
        )
    );

    constructor(private actions$: Actions, private landingService:LandingService){}
}