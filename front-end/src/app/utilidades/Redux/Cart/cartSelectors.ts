import { createSelector } from "@ngrx/store";
import { AppStateInterface } from "../AppStateInterface";

export const selectCartFeature = (state: AppStateInterface) => state.cart;

export const selectCartIsLoading = createSelector(
    selectCartFeature,
    (state) => state.isLoading
);
export const selectCart = createSelector(
    selectCartFeature,
    (state) => state.cart
);
export const selectCartError = createSelector(
    selectCartFeature,
    (state) => state.error
);