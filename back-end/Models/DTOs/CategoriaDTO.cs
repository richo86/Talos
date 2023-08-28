using Models.Enums;

namespace Models.DTOs
{
    public class CategoriaDTO
    {
        public string Id { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }
        public string Area { get; set; }
        public string AreaDescripcion { get; set; }
        public string CategoriaPrincipal { get; set; }
        public string CategoriaPrincipalDescripcion { get; set; }
        public string Imagen { get; set; }
        public string ImagenBase64 { get; set; }
        public TipoCategoria TipoCategoria { get; set; }
        public string AreaId { get; set; }
    }
}
