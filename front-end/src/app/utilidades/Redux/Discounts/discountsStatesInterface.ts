import { productoDTO } from "src/app/productos/productos.models";

export interface discountsStatesInterface{
    isLoading:boolean;
    discounts:productoDTO[];
    error:string | null;
}