import { createSelector } from "@ngrx/store";
import { AppStateInterface } from "../AppStateInterface";

export const selectProductsFeature = (state: AppStateInterface) => state.products;
export const selectProductsFromAreaFeature = (state: AppStateInterface) => state.productsFromArea;
export const selectProductsFromCategoryFeature = (state: AppStateInterface) => state.productsFromCategory;
export const selectProductsFromSubcategoryFeature = (state: AppStateInterface) => state.productsFromSubcategory;

export const selectProductsAreLoading = createSelector(
    selectProductsFeature,
    (state) => state.isLoading
);
export const selectProducts = createSelector(
    selectProductsFeature,
    (state) => state.products
);
export const selectProductsError = createSelector(
    selectProductsFeature,
    (state) => state.error
);
export const selectProductsFromAreaAreLoading = createSelector(
    selectProductsFromAreaFeature,
    (state) => state.isLoading
);
export const selectProductsFromArea = createSelector(
    selectProductsFromAreaFeature,
    (state) => state.products
);
export const selectProductsFromAreaError = createSelector(
    selectProductsFromAreaFeature,
    (state) => state.error
);
export const selectProductsFromCategoryAreLoading = createSelector(
    selectProductsFromCategoryFeature,
    (state) => state.isLoading
);
export const selectProductsFromCategory = createSelector(
    selectProductsFromCategoryFeature,
    (state) => state.products
);
export const selectProductsFromCategoryError = createSelector(
    selectProductsFromCategoryFeature,
    (state) => state.error
);
export const selectProductsFromSubcategoryAreLoading = createSelector(
    selectProductsFromSubcategoryFeature,
    (state) => state.isLoading
);
export const selectProductsFromSubcategory = createSelector(
    selectProductsFromSubcategoryFeature,
    (state) => state.products
);
export const selectProductsFromSubcategoryError = createSelector(
    selectProductsFromSubcategoryFeature,
    (state) => state.error
);
export const selectLatestsProductsAreLoading = createSelector(
    selectProductsFromSubcategoryFeature,
    (state) => state.isLoading
);
export const selectLatestsProducts = createSelector(
    selectProductsFromSubcategoryFeature,
    (state) => state.products
);
export const selectLatestsProductsError = createSelector(
    selectProductsFromSubcategoryFeature,
    (state) => state.error
);