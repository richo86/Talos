import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import * as AreasActions from './areasActions';
import { catchError, map, of, switchMap } from "rxjs";
import { LandingService } from "src/app/landing-page/landing.service";

@Injectable()
export class AreasEffects{
    GetStoreAreas$ = createEffect(() => 
        this.actions$.pipe(
            ofType(AreasActions.getAreas),
            switchMap((action: any) => {
                return this.landingService.GetAreas()
                    .pipe(
                        map((areas) => AreasActions.getAreasSuccess({ CollectionDTO: areas.body })),
                        catchError((errors) => of(AreasActions.getAreasFailure({error: errors.message})))
                    );
            })
        )
    );

    constructor(private actions$: Actions, private landingService:LandingService){}
}