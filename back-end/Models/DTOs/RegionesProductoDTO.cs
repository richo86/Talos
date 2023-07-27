using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class RegionesProductoDTO
    {
        public string Id { get; set; }
        public string Producto { get; set; }
        public string NombreProducto { get; set; }
        public string Imagen { get; set; }
        public List<string> Regiones { get; set; }
        public List<string> RegionesDescripcion { get; set; }
        public List<string> ProductosRelacionados { get; set; }
        public List<string> ProductosRelacionadosDescripcion { get; set; }
        public decimal? Precio { get; set; }
        public int Inventario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string CategoriaDescripcion { get; set; }
        public string SubcategoriaDescripcion { get; set; }
    }
}
