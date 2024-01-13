import { createSelector } from "@ngrx/store";
import { AppStateInterface } from "../AppStateInterface";

export const selectBestSellersFeature = (state: AppStateInterface) => state.bestSellers;

export const selectBestSellersIsLoading = createSelector(
    selectBestSellersFeature,
    (state) => state.isLoading
);
export const BestSellers = createSelector(
    selectBestSellersFeature,
    (state) => state.bestSellers
);
export const selectBestSellersError = createSelector(
    selectBestSellersFeature,
    (state) => state.error
);