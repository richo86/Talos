export class CategoriasProductoDTO
{
    areas:AreasDTO[];

    constructor(){
        this.areas = []
    }
}

export class AreasDTO
{
    id: string;
    descripcion: string;
    dategorias: CategoriaPrincipalDTO[];
}

export class CategoriaPrincipalDTO
{
    id: string;
    descripcion: string;
    codigo: string;
    subcategorias: CategoriaSecundariaDTO[];
}

export class CategoriaSecundariaDTO
{
    id: string;
    descripcion: string;
    codigo: string;
}