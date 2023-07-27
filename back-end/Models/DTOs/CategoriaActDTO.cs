using Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class CategoriaActDTO
    {
        public string Id { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }
        public string Area { get; set; }
        public string AreaDescripcion { get; set; }
        public string CategoriaPrincipal { get; set; }
        public string CategoriaPrincipalDescripcion { get; set; }
        public string Imagen { get; set; }
        public TipoCategoria TipoCategoria { get; set; }
    }
}
