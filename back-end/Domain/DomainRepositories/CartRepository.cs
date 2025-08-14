using Domain.Interfaces;
using Domain.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Classes;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talos.API.User;
using static Models.Utilities.Helper;

namespace Domain.DomainRepositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public CartRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<int> CreateCart(List<Carrito> carrito)
        {
            await context.Carrito.AddRangeAsync(carrito);

            var result = context.SaveChanges();

            if (result > 0)
                return result;
            else
                return 0;
        }

        public async Task<Sesion> CreateSesion(Carrito carrito, string email)
        {
            Sesion sesion = new Sesion();
            Guid idUsuario;

            if (Models.Utilities.Helper.IsGuid(email))
                idUsuario = Guid.Parse(email);
            else
            {
                var persona = await userManager.FindByEmailAsync(email);
                idUsuario = Guid.Parse(persona.Id);
            }

            var sesionActual = context.Sesion.FirstOrDefault(x => x.Estado.Equals(true) && x.IdUsuario.Equals(idUsuario));
            if (sesionActual != null)
                return sesionActual;

            sesion.Id = Guid.NewGuid();
            sesion.FechaCreacion = DateTime.Now;
            sesion.Estado = true;
            sesion.IdUsuario = idUsuario.ToString();
            sesion.TotalCosto = sesion.TotalCosto;

            return sesion;
        }

        public async Task<string> DeleteCart(string id)
        {
            var cart = await context.Carrito.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (cart != null)
            {
                var sesionActive = context.Carrito.Where(x => x.SesionId.Equals(cart.SesionId)).Count() > 1;
                if(!sesionActive)
                {
                    var sesions = context.Sesion.Where(x => x.Id.Equals(cart.SesionId));
                    context.Sesion.RemoveRange(sesions);
                }
                    
                context.Carrito.Remove(cart);
                var result = context.SaveChanges();
                if (result > 0)
                    return "Operación exitosa";
                else
                    return "Ocurrió un error al eliminar el carrito";
            }
            else
                return "No se encontró el carrito que se desea eliminar";
        }

        public async Task<CarritoDTO> GetCart(string id)
        {
            var cart = await context.Carrito.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (cart != null)
            {
                var sesion = context.Sesion.FirstOrDefault(x => x.Id.Equals(cart.SesionId));
                var usuario = await userManager.FindByIdAsync(sesion.IdUsuario.ToString());

                CarritoDTO cartDTO = new CarritoDTO();
                cartDTO.Id = cart.Id.ToString();
                cartDTO.Email = usuario.Email;
                cartDTO.FechaCreacion = cart.FechaCreacion;
                cartDTO.FechaModificacion = cart.FechaModificacion;
                cartDTO.Cantidad = cart.Cantidad;
                cartDTO.ProductoId = cart.ProductoId;
                cartDTO.SesionId = sesion.Id;

                return cartDTO;
            }
                
            else
                return new CarritoDTO();
        }

        public async Task<List<Carrito>> UpdateCart(List<Carrito> carrito)
        {
            foreach (var item in carrito)
            {
                var cart = await context.Carrito.FirstOrDefaultAsync(x => x.Id.Equals(item.Id));
                cart.FechaModificacion = DateTime.Now;

                context.Entry(cart).CurrentValues.SetValues(carrito);
            }

            var result = context.SaveChanges();

            if (result > 0)
                return carrito;
            else
                return new List<Carrito>();
        }
    }
}
