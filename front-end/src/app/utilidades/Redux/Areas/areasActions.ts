import { HttpResponse } from '@angular/common/http';
import {createAction, props} from '@ngrx/store';
import { collectionDTO } from 'src/app/landing-page/landing.models';

export const getAreas  = createAction(
                        '[StoreFrontComponent] Get Areas'
);
export const getAreasSuccess  = createAction(
                            '[StoreFrontComponent] Get Areas Success',
                            props<{CollectionDTO:collectionDTO[]}>()
);
export const getAreasFailure  = createAction(
                            '[StoreFrontComponent] Get Areas Failure',
                            props<{error:string}>()
);