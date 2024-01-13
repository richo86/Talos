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
    public class CampaignRepository : ICampaignRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CampaignRepository(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Campañas> CreateCampaign(Campañas campaign)
        {
            var existingCampaign = context.Campañas.FirstOrDefault(x => x.Nombre.Equals(campaign.Nombre)
                                    && x.Descripcion.Equals(campaign.Descripcion));
            if (existingCampaign != null)
            {
                campaign.FechaEdicion = System.DateTime.Now;
                context.Entry(existingCampaign).CurrentValues.SetValues(campaign);

            }
            else
            {
                campaign.FechaCreacion = System.DateTime.Now;
                await context.Campañas.AddAsync(campaign);
            }

            var result = context.SaveChanges();

            if (result > 0)
                return campaign;
            else
                return new Campañas();
        }

        public async Task<string> DeleteCampaign(string id)
        {
            var campaignId = Guid.Parse(id);
            var campaign = await context.Campañas.FirstOrDefaultAsync(x => x.Id.Equals(campaignId));

            if (campaign != null)
            {
                var campaignProducts = context.CampaignProducts.Where(x => x.CampaignId.Equals(campaignId));
                if(campaignProducts.Any())
                    context.CampaignProducts.RemoveRange(campaignProducts);
                
                context.Campañas.Remove(campaign);
                var result = context.SaveChanges();
                if (result > 0)
                    return "Operación exitosa";
                else
                    return "Ocurrió un error al eliminar la campaña";
            }
            else
                return "No se encontró la campaña que se desea eliminar";
        }

        public List<Campañas> GetAllCampaigns()
        {
            var campaigns = context.Campañas.ToList();

            if (campaigns.Any())
                return campaigns;
            else
                return new List<Campañas>();
        }

        public async Task<CampaignDTO> GetCampaign(string id)
        {
            var campaignId = Guid.Parse(id);
            var campaign = await context.Campañas.FirstOrDefaultAsync(x => x.Id.Equals(campaignId));

            if (campaign != null)
            {
                CampaignDTO existingCampaign = mapper.Map<Campañas,CampaignDTO>(campaign);

                existingCampaign.Productos = context.CampaignProducts.Where(x=>x.CampaignId.Equals(campaignId)).Select(x=>x.ProductId.ToString()).ToList();

                return existingCampaign;
            }
            else
                return new CampaignDTO();
        }

        public IQueryable<Campañas> GetCampaigns()
        {
            var campaigns = context.Campañas.Where(x => x.Descripcion != null);

            if (campaigns.Any())
                return campaigns;
            else
                return new List<Campañas>().AsQueryable();
        }

        public async Task<Campañas> UpdateCampaign(Campañas campaign)
        {
            var campaña = await context.Campañas.FirstOrDefaultAsync(x => x.Id.Equals(campaign.Id));

            if(campaña != null)
                context.Entry(campaña).CurrentValues.SetValues(campaign);

            var result = context.SaveChanges();

            if (!string.IsNullOrEmpty(campaign.Descripcion))
                return campaign;
            else
                return new Campañas();
        }

        public bool CreateImage(string id, string campaign, string base64Image)
        {
            try
            {
                Guid campaignId = Guid.Parse(campaign);
                var existingCampaign = context.Campañas.FirstOrDefault(x => x.Id.Equals(campaignId));

                if (existingCampaign != null)
                {
                    existingCampaign.Imagen = id;
                    existingCampaign.ImagenBase64 = base64Image;
                    context.Campañas.Update(existingCampaign);
                    context.SaveChanges();
                }
                else
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<KeyValuePair<string, string>> getCampaignBase64Images(string id)
        {
            Guid campaignId = Guid.Parse(id);

            var query = (from a in context.Campañas
                         where a.Id == campaignId
                         select new
                         {
                             key = a.Imagen,
                             value = a.ImagenBase64 != null ? a.ImagenBase64 : a.Imagen
                         });

            var images = query.AsEnumerable().Select(x => new KeyValuePair<string, string>(x.key, x.value)).ToList();

            return images;
        }

        public List<ProductoDTO> GetAllProducts()
        {
            return context.Producto.Select(x=> new ProductoDTO { Id = x.Id.ToString(), Nombre = x.Nombre }).ToList();
        }

        public bool InsertCampaignProducts(List<string> products, Guid campaignId)
        {
            var existingProducts = context.CampaignProducts.Where(x=>x.CampaignId == campaignId).ToList();
            if (existingProducts.Any())
            {
                context.RemoveRange(existingProducts);
                context.SaveChanges();
            }

            var campaignProducts = new List<CampaignProducts>();
            foreach (var item in products)
            {
                var newItem = new CampaignProducts()
                {
                    Id = Guid.NewGuid(),
                    ProductId = Guid.Parse(item),
                    CampaignId = campaignId
                };

                campaignProducts.Add(newItem);
            }

            if (campaignProducts.Any())
            {
                context.CampaignProducts.AddRange(campaignProducts);
                context.SaveChanges();
            }

            return true;
        }
    }
}
