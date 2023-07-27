using AutoMapper;
using Domain.Interfaces;
using Domain.Utilities;
using Microsoft.EntityFrameworkCore;
using Models.Classes;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.DomainRepositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public DiscountRepository(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Descuentos> CreateDiscount(Descuentos descuento)
        {
            var existingDiscount = context.Descuentos.FirstOrDefault(x => x.Nombre.Equals(descuento.Nombre) && x.Descripcion.Equals(descuento.Descripcion));
            if (existingDiscount != null)
            {
                descuento.FechaEdicion = System.DateTime.Now;
                context.Entry(existingDiscount).CurrentValues.SetValues(descuento);

            }
            else
            {
                descuento.FechaCreacion = System.DateTime.Now;
                await context.Descuentos.AddAsync(descuento);
            }

            var result = context.SaveChanges();

            if (result > 0)
                return descuento;
            else
                return new Descuentos();
        }

        public async Task<string> DeleteDiscount(string id)
        {
            var descuento = await context.Descuentos.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (descuento != null)
            {
                context.Descuentos.Remove(descuento);
                var result = context.SaveChanges();
                if (result > 0)
                    return "Operación exitosa";
                else
                    return "Ocurrió un error al eliminar el descuento";
            }
            else
                return "No se encontró el descuento que se desea eliminar";
        }

        public async Task<List<Descuentos>> GetAllDiscounts()
        {
            var discounts = await context.Descuentos.ToListAsync();

            if (discounts.Any())
                return discounts;
            else
                return new List<Descuentos>();
        }

        public async Task<DescuentoDTO> GetDiscount(string id)
        {
            var discountId = Guid.Parse(id);
            var discount = await context.Descuentos.FirstOrDefaultAsync(x => x.Id.Equals(discountId));

            if (discount != null)
            {
                DescuentoDTO existingDiscount = mapper.Map<Descuentos,DescuentoDTO>(discount);

                return existingDiscount;
            }
            else
                return new DescuentoDTO();
        }

        public IQueryable<Descuentos> GetDiscounts()
        {
            var discounts = context.Descuentos.Where(x => x.Descripcion != null);

            if (discounts.Any())
                return discounts;
            else
                return new List<Descuentos>().AsQueryable();
        }

        public async Task<Descuentos> UpdateDiscount(Descuentos descuento)
        {
            var discount = await context.Descuentos.FirstOrDefaultAsync(x => x.Id.Equals(descuento.Id));

            if(discount != null)
                context.Entry(discount).CurrentValues.SetValues(descuento);

            var result = context.SaveChanges();

            if (result > 0)
                return descuento;
            else
                return new Descuentos();
        }
    }
}
