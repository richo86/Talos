import { HttpResponse } from '@angular/common/http';
import {createAction, props} from '@ngrx/store';
import { collectionDTO } from 'src/app/landing-page/landing.models';

export const getCategories  = createAction(
                        '[StoreFrontComponent] Get Categories'
);
export const getCategoriesSuccess  = createAction(
                            '[StoreFrontComponent] Get Categories Success',
                            props<{CollectionDTO:collectionDTO[]}>()
);
export const getCategoriesFailure  = createAction(
                            '[StoreFrontComponent] Get Categories Failure',
                            props<{error:string}>()
);