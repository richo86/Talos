import { createReducer, on } from "@ngrx/store";
import * as AreasActions from './areasActions';
import { areasStatesInterface } from "./areasStatesInterface";

export const initialState: areasStatesInterface = {
    isLoading: false,
    areas: [],
    error: null
};

export const areasReducers = createReducer(
    initialState,
    on(AreasActions.getAreas,(state) => ({...state, isLoading : true})),
    on(AreasActions.getAreasSuccess, (state,actions) => ({
        ...state,
        isLoading: false,
        areas: actions.CollectionDTO
    })),
    on(AreasActions.getAreasFailure,(state,actions) => ({
        ...state,
        isLoading:false,
        error: actions.error
    }))
)