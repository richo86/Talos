import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import * as StoreActions from './productActions';
import { catchError, map, of, switchMap } from "rxjs";
import { LandingService } from "src/app/landing-page/landing.service";

@Injectable()
export class ProductsEffects{
    getStoreProducts$ = createEffect(() => 
        this.actions$.pipe(
            ofType(StoreActions.getProducts),
            switchMap((action: any) => {
                return this.landingService.GetStoreProducts(action.countryCode)
                    .pipe(
                        map((products) => StoreActions.getProductsSuccess({ ProductsDTO: products.body })),
                        catchError((errors) => of(StoreActions.getProductsFailure({error: errors.message})))
                    );
            })
        )
    );

    constructor(private actions$: Actions, private landingService:LandingService){}
}

@Injectable()
export class ProductsFromAreaEffects{
    getStoreProducts$ = createEffect(() => 
        this.actions$.pipe(
            ofType(StoreActions.getProductsFromArea),
            switchMap((action: any) => {
                return this.landingService.GetProductsFromSpecificArea(action.countryCode,action.area)
                    .pipe(
                        map((products) => StoreActions.getProductsFromAreaSuccess({ ProductsDTO: products.body })),
                        catchError((errors) => of(StoreActions.getProductsFromAreaFailure({error: errors.message})))
                    );
            })
        )
    );

    constructor(private actions$: Actions, private landingService:LandingService){}
}

@Injectable()
export class ProductsFromCategoryEffects{
    getStoreProducts$ = createEffect(() => 
        this.actions$.pipe(
            ofType(StoreActions.getProductsFromCategory),
            switchMap((action: any) => {
                return this.landingService.GetProductsFromSpecificCategory(action.countryCode,action.category)
                    .pipe(
                        map((products) => StoreActions.getProductsFromCategorySuccess({ ProductsDTO: products.body })),
                        catchError((errors) => of(StoreActions.getProductsFromCategoryFailure({error: errors.message})))
                    );
            })
        )
    );

    constructor(private actions$: Actions, private landingService:LandingService){}
}

@Injectable()
export class ProductsFromSubcategoryEffects{
    getStoreProducts$ = createEffect(() => 
        this.actions$.pipe(
            ofType(StoreActions.getProductsFromSubcategory),
            switchMap((action: any) => {
                return this.landingService.GetStoreProducts(action.countryCode)
                    .pipe(
                        map((products) => StoreActions.getProductsSuccess({ ProductsDTO: products.body })),
                        catchError((errors) => of(StoreActions.getProductsFailure({error: errors.message})))
                    );
            })
        )
    );

    constructor(private actions$: Actions, private landingService:LandingService){}
}