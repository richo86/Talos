using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class NotificationDTO
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public bool? Estado { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Link { get; set; }
    }
}
