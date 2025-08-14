import { productoDTO } from "../productos/productos.models";

export interface campaignDTO{
    id: string;
    nombre: string;
    descripcion: string;
    estado: boolean;
    porcentajeDescuento: number;
    fechaCreacion: Date;
    fechaEdicion: Date;
    fechaInicioVigencia: Date;
    fechaFinVigencia: Date;
    categoria: string;
    subcategoria: string;
    imagen: string;
    imagenBase64: string;
    productos: string[];
}