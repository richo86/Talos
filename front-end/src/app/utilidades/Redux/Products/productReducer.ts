import { productStatesInterface, productsFromAreaInterface, productsFromCategoryInterface, productsFromSubcategoryInterface } from "./productStatesInterface";
import { createReducer, on } from "@ngrx/store";
import * as ProductActions from './productActions';

export const initialState: productStatesInterface = {
    isLoading: false,
    products: [],
    error: null
};

export const productReducers = createReducer(
    initialState,
    on(ProductActions.getProducts,(state) => ({...state, isLoading : true})),
    on(ProductActions.getProductsSuccess, (state,actions) => ({
        ...state,
        isLoading: false,
        cart: actions.ProductsDTO
    })),
    on(ProductActions.getProductsFailure,(state,actions) => ({
        ...state,
        isLoading:false,
        error: actions.error
    }))
)

export const fromAreaInitialState: productsFromAreaInterface = {
    isLoading: false,
    products: [],
    error: null
};

export const productsFromAreaReducers = createReducer(
    fromAreaInitialState,
    on(ProductActions.getProductsFromArea,(state) => ({...state, isLoading : true})),
    on(ProductActions.getProductsFromAreaSuccess, (state,actions) => ({
        ...state,
        isLoading: false,
        cart: actions.ProductsDTO
    })),
    on(ProductActions.getProductsFromAreaFailure,(state,actions) => ({
        ...state,
        isLoading:false,
        error: actions.error
    }))
)

export const fromCategoryInitialState: productsFromCategoryInterface = {
    isLoading: false,
    products: [],
    error: null
};

export const productsFromCategoryReducers = createReducer(
    fromCategoryInitialState,
    on(ProductActions.getProductsFromCategory,(state) => ({...state, isLoading : true})),
    on(ProductActions.getProductsFromCategorySuccess, (state,actions) => ({
        ...state,
        isLoading: false,
        cart: actions.ProductsDTO
    })),
    on(ProductActions.getProductsFromCategoryFailure,(state,actions) => ({
        ...state,
        isLoading:false,
        error: actions.error
    }))
)

export const fromSubcategoryInitialState: productsFromSubcategoryInterface = {
    isLoading: false,
    products: [],
    error: null
};

export const productsFromSubcategoryReducers = createReducer(
    fromAreaInitialState,
    on(ProductActions.getProductsFromSubcategory,(state) => ({...state, isLoading : true})),
    on(ProductActions.getProductsFromSubcategorySuccess, (state,actions) => ({
        ...state,
        isLoading: false,
        cart: actions.ProductsDTO
    })),
    on(ProductActions.getProductsFromSubcategoryFailure,(state,actions) => ({
        ...state,
        isLoading:false,
        error: actions.error
    }))
)