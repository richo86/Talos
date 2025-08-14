using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Classes
{
    public class Campañas
    {
        public Guid Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public Guid Categoria { get; set; }
        public Guid Subcategoria { get; set; }
        public bool Estado { get; set; }
        [Range(0, 100)]
        public decimal PorcentajeDescuento { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaEdicion { get; set; }
        public DateTime FechaInicioVigencia { get; set; }
        public DateTime FechaFinVigencia { get; set; }
        public string Imagen { get; set; }
        public string ImagenBase64 { get; set; }

        private void DeactivateDiscount()
        {
            Estado = false;
            FechaEdicion = DateTime.Now;
        }
    }
}
