import { HttpResponse } from '@angular/common/http';
import {createAction, props} from '@ngrx/store';
import { productoDTO } from 'src/app/productos/productos.models';

export const getLowestCost  = createAction(
                        '[StoreFrontComponent] Get LowestCost',
                        props<{ countryCode:string }>()
);
export const getLowestCostSuccess  = createAction(
                            '[StoreFrontComponent] Get LowestCost Success',
                            props<{ProductsDTO:productoDTO[]}>()
);
export const getLowestCostFailure  = createAction(
                            '[StoreFrontComponent] Get LowestCost Failure',
                            props<{error:string}>()
);