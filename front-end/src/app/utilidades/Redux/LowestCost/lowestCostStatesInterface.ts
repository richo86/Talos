import { productoDTO } from "src/app/productos/productos.models";

export interface lowestCostStatesInterface{
    isLoading:boolean;
    lowestCost:productoDTO[];
    error:string | null;
}