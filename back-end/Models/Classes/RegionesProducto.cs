using System;
using System.Collections.Generic;
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
        public decimal? Precio { get; set; }
        public int Inventario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
