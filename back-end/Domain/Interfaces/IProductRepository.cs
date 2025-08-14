using Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProductRepository
    {
        IQueryable<Producto> GetProducts();
        Task<Producto> GetProduct(string id);
        List<string> getProductIds(string id);
        List<KeyValuePair<string, string>> getProductBase64Images(string id);
        Task<Producto> CreateProduct(Producto producto);
        Task<Producto> UpdateProduct(Producto producto);
        Task<string> DeleteProduct(string id);
        bool CreateImage(string id, string product, string base64);
        bool AssignKeywords(List<string> keywords, Guid id);
        List<string> GetProductKeywords(Guid id);
    }
}
