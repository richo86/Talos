import { createReducer, on } from "@ngrx/store";
import * as CategoriesActions from './categoriesActions';
import { categoriesStatesInterface } from "./categoriesStatesInterface";

export const initialState: categoriesStatesInterface = {
    isLoading: false,
    categories: [],
    error: null
};

export const categoriesReducers = createReducer(
    initialState,
    on(CategoriesActions.getCategories,(state) => ({...state, isLoading : true})),
    on(CategoriesActions.getCategoriesSuccess, (state,actions) => ({
        ...state,
        isLoading: false,
        areas: actions.CollectionDTO
    })),
    on(CategoriesActions.getCategoriesFailure,(state,actions) => ({
        ...state,
        isLoading:false,
        error: actions.error
    }))
)