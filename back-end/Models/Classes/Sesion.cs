using System;
using Talos.API.User;

namespace Models.Classes
{
    public class Sesion
    {
        public Guid Id { get; set; }
        public Guid IdUsuario { get; set; }
        public decimal TotalCosto { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
