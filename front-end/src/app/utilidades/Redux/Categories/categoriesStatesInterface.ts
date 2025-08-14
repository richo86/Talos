import { collectionDTO } from "src/app/landing-page/landing.models";

export interface categoriesStatesInterface{
    isLoading:boolean;
    categories:collectionDTO[];
    error:string | null;
}