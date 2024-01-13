using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class CalificacionDTO
    {
        public Guid ProductoId { get; set; }
        [Range(1,5)]
        public int Puntuacion { get; set; }
        public string UsuarioId { get; set; }
    }
}
