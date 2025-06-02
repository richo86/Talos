import { HttpResponse } from '@angular/common/http';
import {createAction, props} from '@ngrx/store';
import { productoDTO } from 'src/app/productos/productos.models';

export const getProducts  = createAction(
        '[StoreFrontComponent] Get Products',
        props<{countryCode:string}>()
);
export const getProductsSuccess  = createAction(
        '[StoreFrontComponent] Get Products Success',
        props<{ProductsDTO:productoDTO[]}>()
);
export const getProductsFailure  = createAction(
        '[StoreFrontComponent] Get Products Failure',
        props<{error:string}>()
);
export const getProductsFromArea  = createAction(
    '[StoreFrontComponent] Get Products from area',
    props<{countryCode:string,area:string}>()
);
export const getProductsFromAreaSuccess  = createAction(
        '[StoreFrontComponent] Get Products from area Success',
        props<{ProductsDTO:productoDTO[]}>()
);
export const getProductsFromAreaFailure  = createAction(
        '[StoreFrontComponent] Get Products from area Failure',
        props<{error:string}>()
);
export const getProductsFromCategory  = createAction(
    '[StoreFrontComponent] Get Products from category',
    props<{countryCode:string,category:string}>()
);
export const getProductsFromCategorySuccess  = createAction(
        '[StoreFrontComponent] Get Products from category Success',
        props<{ProductsDTO:productoDTO[]}>()
);
export const getProductsFromCategoryFailure  = createAction(
        '[StoreFrontComponent] Get Products from category Failure',
        props<{error:string}>()
);
export const getProductsFromSubcategory  = createAction(
    '[StoreFrontComponent] Get Products from subcategory',
    props<{countryCode:string,subcategory:string}>()
);
export const getProductsFromSubcategorySuccess  = createAction(
        '[StoreFrontComponent] Get Products from subcategory Success',
        props<{ProductsDTO:productoDTO[]}>()
);
export const getProductsFromSubcategoryFailure  = createAction(
        '[StoreFrontComponent] Get Products from subcategory Failure',
        props<{error:string}>()
);
export const getLatestProducts  = createAction(
        '[StoreFrontComponent] Get Latests Products',
        props<{countryCode:string,subcategory:string}>()
);
export const getLatestProductsSuccess  = createAction(
        '[StoreFrontComponent] Get Latests Products Success',
        props<{ProductsDTO:productoDTO[]}>()
);
export const getLatestsProductsFailure  = createAction(
        '[StoreFrontComponent] Get Latests Products Failure',
        props<{error:string}>()
);