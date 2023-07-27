import { FlexibleConnectedPositionStrategyOrigin } from "@angular/cdk/overlay";

export interface CategoriaDTO
{
    id:string;
    descripcion:string;
    codigo:string;
    tipoCategoria:number;
    area:string;
    areaDescripcion:string;
    categoriaPrincipal:string;
    categoriaPrincipalDescripcion:string;
    imagen:string;
}

export class TipoCategoria{
    value:number;
    descripcion:string;
}