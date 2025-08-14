using Domain.Interfaces;
using Domain.Utilities;
using Models.Classes;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.DomainRepositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly ApplicationDbContext context;

        public RatingRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task CreateOrUpdate(Calificacion calificacion)
        {
            var calificacionExistente = context.Calificaciones.FirstOrDefault(x=>x.ProductoId.Equals(calificacion.ProductoId) 
                                    && x.UsuarioId.Equals(calificacion.UsuarioId));

            if(calificacionExistente == null)
            {
                await context.Calificaciones.AddAsync(calificacion);
            }
            else
            {
                calificacionExistente.Puntuacion = calificacion.Puntuacion;
                context.Calificaciones.Update(calificacionExistente);
            }

            await context.SaveChangesAsync();
        }
    }
}
