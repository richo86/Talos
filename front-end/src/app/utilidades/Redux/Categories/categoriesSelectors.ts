import { createSelector } from "@ngrx/store";
import { AppStateInterface } from "../AppStateInterface";

export const selectCategoriesFeature = (state: AppStateInterface) => state.categories;

export const selectCategoriesIsLoading = createSelector(
    selectCategoriesFeature,
    (state) => state.isLoading
);
export const selectCategories = createSelector(
    selectCategoriesFeature,
    (state) => state.categories
);
export const selectCategoriesError = createSelector(
    selectCategoriesFeature,
    (state) => state.error
);