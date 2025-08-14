using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class ProductosRelacionadosDTO
    {
        public string ID { get; set; }
        public string Producto { get; set; }
        public string ProductoRelacionado { get; set; }
    }
}
