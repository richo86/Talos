import { productoDTO } from "src/app/productos/productos.models";

export interface productStatesInterface{
    isLoading:boolean;
    products:productoDTO[];
    error:string | null;
}

export interface productsFromAreaInterface{
    isLoading:boolean;
    products:productoDTO[];
    error:string | null;
}

export interface productsFromCategoryInterface{
    isLoading:boolean;
    products:productoDTO[];
    error:string | null;
}

export interface productsFromSubcategoryInterface{
    isLoading:boolean;
    products:productoDTO[];
    error:string | null;
}