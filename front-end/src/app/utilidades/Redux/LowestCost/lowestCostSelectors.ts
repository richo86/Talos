import { createSelector } from "@ngrx/store";
import { AppStateInterface } from "../AppStateInterface";

export const selectLowestCostFeature = (state: AppStateInterface) => state.lowestCost;

export const selectLowestCostAreLoading = createSelector(
    selectLowestCostFeature,
    (state) => state.isLoading
);
export const selectLowestCost = createSelector(
    selectLowestCostFeature,
    (state) => state.lowestCost
);
export const selectLowestCostError = createSelector(
    selectLowestCostFeature,
    (state) => state.error
);