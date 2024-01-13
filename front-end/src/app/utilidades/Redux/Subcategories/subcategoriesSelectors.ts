import { createSelector } from "@ngrx/store";
import { AppStateInterface } from "../AppStateInterface";

export const selectSubcategoriesFeature = (state: AppStateInterface) => state.subcategories;

export const selectSubcategoriesIsLoading = createSelector(
    selectSubcategoriesFeature,
    (state) => state.isLoading
);
export const selectSubcategories = createSelector(
    selectSubcategoriesFeature,
    (state) => state.subcategories
);
export const selectSubcategoriesError = createSelector(
    selectSubcategoriesFeature,
    (state) => state.error
);