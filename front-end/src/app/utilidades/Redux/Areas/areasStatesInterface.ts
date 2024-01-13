import { collectionDTO } from "src/app/landing-page/landing.models";

export interface areasStatesInterface{
    isLoading:boolean;
    areas:collectionDTO[];
    error:string | null;
}