import { productoDTO } from "src/app/productos/productos.models";

export interface bestStatesInterface{
    isLoading:boolean;
    bestSellers:productoDTO[];
    error:string | null;
}