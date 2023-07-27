using Models.Enums;
using System;
using System.Collections.Generic;

namespace Models.DTOs
{
    public class CategoriasProductoDTO
    {
        public List<AreasDTO> Areas { get; set; }
    }

    public class AreasDTO
    {
        public string Id { get; set; }
        public string Descripcion { get; set; }
        public List<CategoriaPrincipalDTO> Categorias { get; set; }
    }

    public class CategoriaPrincipalDTO
    {
        public string Id { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }
        public List<CategoriaSecundariaDTO> Subcategorias { get; set; }
    }

    public class CategoriaSecundariaDTO
    {
        public string Id { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }
    }
}
