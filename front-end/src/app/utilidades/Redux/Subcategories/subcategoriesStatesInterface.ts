import { collectionDTO } from "src/app/landing-page/landing.models";

export interface subcategoriesStatesInterface{
    isLoading:boolean;
    subcategories:collectionDTO[];
    error:string | null;
}