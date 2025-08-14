using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talos.API.User;

namespace Models.Classes
{
    public class Mensajes
    {
        public Guid Id { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaRegistro { get; set; }
        [EmailAddress]
        public string UsuarioEmail { get; set; }
        [EmailAddress]
        public string UsuarioReceptorEmail { get; set; }
    }
}
