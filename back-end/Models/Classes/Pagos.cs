using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Classes
{
    public class Pagos
    {
        public Guid Id { get; set; }
        [Range(double.Epsilon, double.MaxValue)]
        public decimal Valor { get; set; }
        public string MetodoPago { get; set; }
        public bool Estado { get; set; }
        public string Observaciones { get; set; }
        public Guid TipoVenta { get; set; }
        public DateTime FechaEstimada { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
