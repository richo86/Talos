import { HttpResponse } from '@angular/common/http';
import {createAction, props} from '@ngrx/store';
import { collectionDTO } from 'src/app/landing-page/landing.models';
import { productoDTO } from 'src/app/productos/productos.models';

export const getBestSellers  = createAction(
                        '[StoreFrontComponent] Get Best Sellers',
                        props<{ countryCode:string }>()
);
export const getBestSellersSuccess  = createAction(
                            '[StoreFrontComponent] Get Best Sellers Success',
                            props<{ProductsDTO:productoDTO[]}>()
);
export const getGetBestSellersFailure  = createAction(
                            '[StoreFrontComponent] Get Best Sellers Failure',
                            props<{error:string}>()
);