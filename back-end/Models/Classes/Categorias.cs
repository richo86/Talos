using Models.Classes;
using Models.Enums;
using System;
using System.Collections.Generic;

namespace Models
{
    public class Categorias
    {
        public Guid Id { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }
        public Guid Area { get; set; }
        public string Imagen { get; set; }
        public TipoCategoria TipoCategoria { get; set; }
        public ICollection<Producto> Producto { get; set; }
    }
}
