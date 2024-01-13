import { HttpResponse } from '@angular/common/http';
import {createAction, props} from '@ngrx/store';
import { CartDTO } from 'src/app/cart/cartDTO.models';

export const getCart  = createAction(
                        '[CartComponent] Get Cart',
                        props<{ id:string }>()
);
export const getCartSuccess  = createAction(
                            '[CartComponent] Get Cart Success',
                            props<{CartDTO:CartDTO[]}>()
);
export const getCartFailure  = createAction(
                            '[CartComponent] Get Cart Failure',
                            props<{error:string}>()
);