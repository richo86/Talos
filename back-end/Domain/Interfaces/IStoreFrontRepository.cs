using Models;
using Models.Classes;
using Models.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IStoreFrontRepository
    {
        Task<CategoriasProductoDTO> GetAllMenuItems();
        List<ProductoDTO> GetLatestProducts(string countryCode);
        List<ProductoDTO> GetBestSellers(string countryCode);
        List<ProductoDTO> GetProductsFromCategory(string countryCode);
        List<ProductoDTO> GetProductsFromSubcategory(string countryCode);
        List<ProductoDTO> GetProductsFromArea(string countryCode);
        List<ProductoDTO> GetProductsFromSpecificArea(string countryCode, string area);
        List<ProductoDTO> GetProductsFromSpecificCategory(string countryCode, string category);
        List<ProductoDTO> GetProductsFromSpecificSubcategory(string countryCode, string subcategory);
    }
}
