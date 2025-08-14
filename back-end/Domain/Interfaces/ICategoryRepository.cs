using Models;
using Models.Classes;
using Models.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICategoryRepository
    {
        IQueryable<CategoriaDTO> GetCategories();
        IQueryable<CategoriaDTO> GetSubcategories();
        IQueryable<CategoriaDTO> GetAreas();
        Task<Categorias> GetCategory(string id);
        Task<Areas> GetArea(string id);
        Task<Subcategorias> GetSecondaryCategory(string id);
        Task<Areas> CreateArea(Areas area);
        Task<Categorias> CreateCategory(Categorias categoria);
        Task<Subcategorias> CreateSubCategory(Subcategorias categorias);
        Task<Areas> UpdateArea(Areas area);
        Task<Categorias> UpdateCategory(Categorias categoria);
        Task<Subcategorias> UpdateSubcategory(Subcategorias categoria);
        Task<string> DeleteCategory(string id);
        Task<List<Areas>> GetMainAreas();
        Task<List<Categorias>> GetMainCategories();
        List<Subcategorias> GetSecondaryCategories(string category);
        bool CreateImage(string id, string category, string imageBase64);
        Task<List<CategoriaDTO>> GetCategoriesFromArea(string id);
        Task<List<CategoriaDTO>> GetSubcategoriesFromCategory(string id);
    }
}
