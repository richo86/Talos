using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talos.API.User;

namespace Models.Classes
{
    public class Pais
    {
        public Guid Id { get; set; }
        public string Iso { get; set; }
        public string Nombre { get; set; }
        public string NombreMin { get; set; }
        public string Abreviacion { get; set; }
        public string NumCode { get; set; }
        public string Codigo { get; set; }
        public decimal? PesoCosto { get; set; }
    }
}
