export interface descuentoDTO{
    id: string;
    nombre: string;
    descripcion: string;
    estado: boolean;
    porcentajeDescuento: number;
    fechaCreacion: Date;
    fechaEdicion: Date;
    codigoPromocion: string;
    fechaInicioVigencia: Date;
    fechaFinVigencia: Date;
}