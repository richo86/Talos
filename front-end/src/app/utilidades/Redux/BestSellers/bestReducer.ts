import { createReducer, on } from "@ngrx/store";
import * as BestActions from './bestActions';
import { bestStatesInterface } from "./bestStatesInterface";

export const initialState: bestStatesInterface = {
    isLoading: false,
    bestSellers: [],
    error: null
};

export const bestReducers = createReducer(
    initialState,
    on(BestActions.getBestSellers,(state) => ({...state, isLoading : true})),
    on(BestActions.getBestSellersSuccess, (state,actions) => ({
        ...state,
        isLoading: false,
        bestSellers: actions.ProductsDTO
    })),
    on(BestActions.getGetBestSellersFailure,(state,actions) => ({
        ...state,
        isLoading:false,
        error: actions.error
    }))
)