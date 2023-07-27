using Models.Classes;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRatingRepository
    {
        Task CreateOrUpdate(Calificacion calificacion);
    }
}
