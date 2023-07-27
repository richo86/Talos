export interface regionesProductoDTO{
    id : string,
    producto : string, 
    nombreProducto : string,
    imagen : string,
    regiones : string[], 
    productosRelacionados : string[];
    precio : number,
    inventario: number,
    fechaCreacion : Date,
    fechaModificacion : Date,
    categoriaDescripcion : string,
    subcategoriaDescripcion : string
}

export interface productosRelacionadosDTO{
    ID : string,
    Producto : string,
    ProductoRelacionado : string
}