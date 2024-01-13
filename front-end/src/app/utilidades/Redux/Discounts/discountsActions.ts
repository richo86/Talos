import { HttpResponse } from '@angular/common/http';
import {createAction, props} from '@ngrx/store';
import { productoDTO } from 'src/app/productos/productos.models';

export const getDiscounts  = createAction(
                        '[StoreFrontComponent] Get Discounts',
                        props<{ countryCode:string }>()
);
export const getDiscountsSuccess  = createAction(
                            '[StoreFrontComponent] Get Discounts Success',
                            props<{ProductsDTO:productoDTO[]}>()
);
export const getDiscountsFailure  = createAction(
                            '[StoreFrontComponent] Get Discounts Failure',
                            props<{error:string}>()
);