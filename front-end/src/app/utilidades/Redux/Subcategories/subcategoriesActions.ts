import { HttpResponse } from '@angular/common/http';
import {createAction, props} from '@ngrx/store';
import { collectionDTO } from 'src/app/landing-page/landing.models';

export const getSubcategories  = createAction(
                        '[StoreFrontComponent] Get Subcategories'
);
export const getSubcategoriesSuccess  = createAction(
                            '[StoreFrontComponent] Get Subcategories Success',
                            props<{CollectionDTO:collectionDTO[]}>()
);
export const getSubcategoriesFailure  = createAction(
                            '[StoreFrontComponent] Get Subcategories Failure',
                            props<{error:string}>()
);