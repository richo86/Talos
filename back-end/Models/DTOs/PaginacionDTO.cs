using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class PaginacionDTO
    {
        public int Pagina { get; set; } = 1;
        private int registrosPorPagina = 10;
        private readonly int cantidadMaxima = 50;

        public int RegistrosPorPagina
        {
            get
            {
                return registrosPorPagina;
            }
            set
            {
                registrosPorPagina = (value > cantidadMaxima) ? cantidadMaxima : value;
            }
        }
    }
}
