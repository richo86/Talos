import { createSelector } from "@ngrx/store";
import { AppStateInterface } from "../AppStateInterface";

export const selectAreasFeature = (state: AppStateInterface) => state.areas;

export const selectAreasIsLoading = createSelector(
    selectAreasFeature,
    (state) => state?.isLoading
);
export const selectAreas = createSelector(
    selectAreasFeature,
    (state) => state?.areas
);
export const selectAreasError = createSelector(
    selectAreasFeature,
    (state) => state?.error
);