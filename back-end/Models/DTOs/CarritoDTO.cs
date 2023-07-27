using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class CarritoDTO
    {
        public string Id { get; set; }
        public Guid? SesionId { get; set; }
        public Guid? ProductoId { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string Email { get; set; }
    }

}
