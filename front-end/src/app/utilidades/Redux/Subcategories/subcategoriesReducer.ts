import { createReducer, on } from "@ngrx/store";
import * as SubcategoriesActions from './subcategoriesActions';
import { subcategoriesStatesInterface } from "./subcategoriesStatesInterface";

export const initialState: subcategoriesStatesInterface = {
    isLoading: false,
    subcategories: [],
    error: null
};

export const subcategoriesReducers = createReducer(
    initialState,
    on(SubcategoriesActions.getSubcategories,(state) => ({...state, isLoading : true})),
    on(SubcategoriesActions.getSubcategoriesSuccess, (state,actions) => ({
        ...state,
        isLoading: false,
        subcategories: actions.CollectionDTO
    })),
    on(SubcategoriesActions.getSubcategoriesFailure,(state,actions) => ({
        ...state,
        isLoading:false,
        error: actions.error
    }))
)