import { createSelector } from "@ngrx/store";
import { AppStateInterface } from "../AppStateInterface";

export const selectDiscountsFeature = (state: AppStateInterface) => state.discounts;

export const selectDiscountsAreLoading = createSelector(
    selectDiscountsFeature,
    (state) => state.isLoading
);
export const selectDiscounts = createSelector(
    selectDiscountsFeature,
    (state) => state.discounts
);
export const selectDiscountsError = createSelector(
    selectDiscountsFeature,
    (state) => state.error
);