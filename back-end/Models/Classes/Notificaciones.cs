using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talos.API.User;

namespace Models.Classes
{
    public class Notificaciones
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public bool Estado { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Link { get; set; }
    }
}
