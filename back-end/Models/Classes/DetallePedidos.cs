using System;

namespace Models.Classes
{
    public class DetallePedidos
    {
        public Guid Id { get; set; }
        public string UsuarioId { get; set; }
        public decimal ValorTotal { get; set; }
        public Guid PagoId { get; set; }
        public Pagos Pago { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
