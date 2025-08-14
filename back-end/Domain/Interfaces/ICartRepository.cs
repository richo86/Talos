using Models;
using Models.Classes;
using Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICartRepository
    {
        Task<CarritoDTO> GetCart(string id);
        Task<int> CreateCart(List<Carrito> carrito);
        Task<Sesion> CreateSesion(Carrito carrito, string Email);
        Task<List<Carrito>> UpdateCart(List<Carrito> carrito);
        Task<string> DeleteCart(string id);
    }
}
