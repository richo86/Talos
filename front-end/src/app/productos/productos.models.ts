export interface productoDTO{
    id: string;
    nombre: string;
    descripcion: string;
    inventario: string;
    precio: string;
    imagen: string[];
    ImagenesBase64: KeyValuePair<string,string>;
    fechaCreacion: Date;
    fechaModificacion: Date;
    categoriaId: string;
    categoriaDescripcion: string;
    subcategoriaId: string;
    subcategoriaDescripcion: string;
    descuentoId: string;
    codigo: string;
}

export interface crearProductoDTO{
    id: string;
    nombre: string;
    descripcion: string;
    inventario: string;
    precio: string;
    fechaCreacion: Date;
    fechaModificacion: Date;
    categoriaId: string;
    categoriaDescripcion: string;
    subcategoriaId: string;
    subcategoriaDescripcion: string;
    descuentoId: string;
    codigo: string;
}

export interface KeyValuePair<K, V> {
    key: K;
    value: V;
}