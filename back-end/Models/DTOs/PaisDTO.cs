using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class PaisDTO
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
    }
}
