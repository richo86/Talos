using System;
using System.Collections.Generic;

namespace Models.DTOs
{
    public class ProductoDTO
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Inventario { get; set; }
        public decimal Precio { get; set; }
        public string Moneda { get; set; }
        public List<string> Imagenes { get; set; }
        public List<KeyValuePair<string, string>> ImagenesBase64 { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string AreaId { get; set; }
        public string AreaDescripcion { get; set; }
        public string CategoriaId { get; set; }
        public string CategoriaDescripcion { get; set; }
        public string SubcategoriaId { get; set; }
        public string SubcategoriaDescripcion { get; set; }
        public string DescuentoId { get; set; }
        public string ValorDescuento { get; set; }
        public string Codigo { get; set; }
        public List<string> Keywords { get; set; }
    }
}
