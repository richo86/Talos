using Models;
using Models.Classes;
using Models.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IDiscountRepository
    {
        IQueryable<Descuentos> GetDiscounts();
        List<Descuentos> GetAllDiscounts();
        Task<DescuentoDTO> GetDiscount(string id);
        Task<Descuentos> CreateDiscount(Descuentos descuentos);
        Task<Descuentos> UpdateDiscount(Descuentos descuento);
        Task<string> DeleteDiscount(string id);
    }
}
