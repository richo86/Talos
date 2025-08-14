import { createReducer, on } from "@ngrx/store";
import * as LowestCostActions from './lowestCostActions';
import { lowestCostStatesInterface } from "./lowestCostStatesInterface";

export const initialState: lowestCostStatesInterface = {
    isLoading: false,
    lowestCost: [],
    error: null
};

export const lowestCostReducers = createReducer(
    initialState,
    on(LowestCostActions.getLowestCost,(state) => ({...state, isLoading : true})),
    on(LowestCostActions.getLowestCostSuccess, (state,actions) => ({
        ...state,
        isLoading: false,
        lowestCost: actions.ProductsDTO
    })),
    on(LowestCostActions.getLowestCostFailure,(state,actions) => ({
        ...state,
        isLoading:false,
        error: actions.error
    }))
)