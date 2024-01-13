import { CartDTO } from "src/app/cart/cartDTO.models";

export interface cartStatesInterface{
    isLoading:boolean;
    cart:CartDTO[];
    error:string | null;
}