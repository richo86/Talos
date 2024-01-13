using Models;
using Models.Classes;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICampaignRepository
    {
        IQueryable<Campañas> GetCampaigns();
        List<Campañas> GetAllCampaigns();
        Task<CampaignDTO> GetCampaign(string id);
        Task<Campañas> CreateCampaign(Campañas campaña);
        Task<Campañas> UpdateCampaign(Campañas campaña);
        Task<string> DeleteCampaign(string id);
        bool CreateImage(string id, string campaign, string base64Image);
        List<KeyValuePair<string, string>> getCampaignBase64Images(string id);
        List<ProductoDTO> GetAllProducts();
        bool InsertCampaignProducts(List<string> products, Guid campaignId);
    }
}
