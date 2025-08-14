import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import * as SubcategoriesActions from './subcategoriesActions';
import { catchError, map, of, switchMap } from "rxjs";
import { LandingService } from "src/app/landing-page/landing.service";

@Injectable()
export class SubcategoriesEffects{
    GetStoreSubcategories$ = createEffect(() => 
        this.actions$.pipe(
            ofType(SubcategoriesActions.getSubcategories),
            switchMap((action: any) => {
                return this.landingService.GetStoreSubcategories()
                    .pipe(
                        map((subcategories) => SubcategoriesActions.getSubcategoriesSuccess({ CollectionDTO: subcategories.body })),
                        catchError((errors) => of(SubcategoriesActions.getSubcategoriesFailure({error: errors.message})))
                    );
            })
        )
    );

    constructor(private actions$: Actions, private landingService:LandingService){}
}