using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talos.API.User;

namespace Models.Classes
{
    public class Carrito
    {
        public Guid Id { get; set; }
        public Guid SesionId { get; set; }
        public Sesion Sesion { get; set; }
        public Guid ProductoId { get; set; }
        public Producto Producto { get; set; }
        [Range(1,30)]
        public int Cantidad { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
