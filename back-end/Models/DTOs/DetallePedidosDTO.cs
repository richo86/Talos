using System;
using System.Collections.Generic;

namespace Models.DTOs
{
    public class DetallePedidosDTO
    {
        public string Id { get; set; }
        public string NombreUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public decimal TotalVenta { get; set; }
        public string MetodoPago { get; set; }
        public bool Estado { get; set; }
        public string Observaciones { get; set; }
        public string TipoVenta { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public List<ItemsPedidoDTO> ItemsPedido { get; set; }
    }

    public class ItemsPedidoDTO
    {
        public Guid ProductoId { get; set; }
        public string Producto { get; set; }
        public string Imagen { get; set; }
        public decimal Valor { get; set; }
    }
}
