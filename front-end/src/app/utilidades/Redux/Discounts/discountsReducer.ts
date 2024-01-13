import { createReducer, on } from "@ngrx/store";
import * as DiscountsActions from './discountsActions';
import { discountsStatesInterface } from "./discountsStatesInterface";

export const initialState: discountsStatesInterface = {
    isLoading: false,
    discounts: [],
    error: null
};

export const discountsReducers = createReducer(
    initialState,
    on(DiscountsActions.getDiscounts,(state) => ({...state, isLoading : true})),
    on(DiscountsActions.getDiscountsSuccess, (state,actions) => ({
        ...state,
        isLoading: false,
        discounts: actions.ProductsDTO
    })),
    on(DiscountsActions.getDiscountsFailure,(state,actions) => ({
        ...state,
        isLoading:false,
        error: actions.error
    }))
)