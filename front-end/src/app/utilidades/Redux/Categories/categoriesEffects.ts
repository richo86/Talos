import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import * as CategoriesActions from './categoriesActions';
import { catchError, map, of, switchMap } from "rxjs";
import { LandingService } from "src/app/landing-page/landing.service";

@Injectable()
export class CategoriesEffects{
    GetStoreCategories$ = createEffect(() => 
        this.actions$.pipe(
            ofType(CategoriesActions.getCategories),
            switchMap((action: any) => {
                return this.landingService.GetStoreCategories()
                    .pipe(
                        map((categories) => CategoriesActions.getCategoriesSuccess({ CollectionDTO: categories.body })),
                        catchError((errors) => of(CategoriesActions.getCategoriesFailure({error: errors.message})))
                    );
            })
        )
    );

    constructor(private actions$: Actions, private landingService:LandingService){}
}