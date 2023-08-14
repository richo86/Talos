using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Classes
{
    public class RegionesProducto
    {
        public Guid Id { get; set; }
        public Guid Producto { get; set; }
        public Guid Pais { get; set; }
        [Range(double.Epsilon, double.MaxValue)]
        public decimal? Precio { get; set; }
        [Range(1, int.MaxValue)]
        public int Inventario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
