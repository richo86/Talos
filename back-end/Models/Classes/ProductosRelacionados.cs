using System;

namespace Models.Classes
{
    public class ProductosRelacionados
    {
        public Guid ID { get; set; }
        public Guid Producto { get; set; }
        public Guid ProductoRelacionado { get; set; }
    }
}
