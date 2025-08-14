using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class PaymentDTO
    {
        public string Id { get; set; }
        public decimal Valor { get; set; }
        public string MetodoPago { get; set; }
        public bool Estado { get; set; }
        public string Observaciones { get; set; }
        public string TipoVenta { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public DateTime FechaEstimada { get; set; }

        public List<CarritoDTO> Carrito { get; set; }
        public UsuarioDTO Usuario { get; set; }
    }
}
