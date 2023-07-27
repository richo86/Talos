using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talos.API.User;

namespace Models.Classes
{
    public class Calificacion
    {
        public Guid Id { get; set; }
        [Range(1,5)]
        public int Puntuacion { get; set; }
        public Guid ProductoId { get; set; }
        public Producto Producto { get; set; }
        public string UsuarioId { get; set; }
        public ApplicationUser Usuario { get; set; }
    }
}
