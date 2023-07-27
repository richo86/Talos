using System;

namespace Models.Classes
{
    public class ItemsPedido
    {
        public Guid Id { get; set; }
        public Guid DetallePedidosId { get; set; }
        public DetallePedidos DetallePedidos { get; set; }
        public Guid ProductoId { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
