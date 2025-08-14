import { CartDTO } from "src/app/cart/cartDTO.models";
import { cartStatesInterface } from "./cartStatesInterface";
import { createReducer, on } from "@ngrx/store";
import * as CartActions from './cartActions';

export const initialState: cartStatesInterface = {
    isLoading: false,
    cart: [],
    error: null
};

export const cartReducers = createReducer(
    initialState,
    on(CartActions.getCart,(state) => ({...state, isLoading : true})),
    on(CartActions.getCartSuccess, (state,actions) => ({
        ...state,
        isLoading: false,
        cart: actions.CartDTO
    })),
    on(CartActions.getCartFailure,(state,actions) => ({
        ...state,
        isLoading:false,
        error: actions.error
    }))
)