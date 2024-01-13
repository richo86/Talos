using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Models.DTOs
{
    public class CrearProductoDTO
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public long Inventario { get; set; }
        public decimal Precio { get; set; }
        public string Moneda { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string CategoriaId { get; set; }
        public string CategoriaDescripcion { get; set; }
        public string SubcategoriaId { get; set; }
        public string SubcategoriaDescripcion { get; set; }
        public string DescuentoId { get; set; }
        public List<string> Keywords { get; set; }
    }
}
