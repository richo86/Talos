using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talos.API.User;

namespace Models.Classes
{
    public class Imagenes
    {
        public Guid Id { get; set; }
        public Guid? ProductoId { get; set; }
        public string ImagenUrl { get; set; }
        public string ImagenBase64 { get; set; }
    }
}
