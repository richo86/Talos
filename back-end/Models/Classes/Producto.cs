using System;
using System.Collections.Generic;

namespace Models.Classes
{
    public class Producto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }
        public decimal Precio { get; set; }
        public long Inventario { get; set; }
        public Guid? DescuentoId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public Guid CategoriaId { get; set; }
        public List<Imagenes> Imagenes { get; set; }
        public Categorias Categoria { get; set; }
        public Guid SubcategoriaId { get; set; }
        public Subcategorias Subcategoria { get; set; }
        public ICollection<ItemsPedido> ItemsPedido { get; set; }
    }
}
