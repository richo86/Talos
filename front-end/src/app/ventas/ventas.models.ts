export class ventasDTO{
    id:string;
    nombreUsuario: string;
    emailUsuario: string;
    totalVenta: number;
    totalVentaSinIVA: number;
    metodoPago: string;
    estado: boolean;
    observaciones: string;
    tipoVenta:string;
    fechaActualizacion:Date;
    itemsPedido:ItemsPedidoDTO[];
}

export class ItemsPedidoDTO{
    productoId:string;
    producto:string;
    imagen:string;
    valor:number;
}

export class PaymentType{
    id:string;
    descripcion:string;
}