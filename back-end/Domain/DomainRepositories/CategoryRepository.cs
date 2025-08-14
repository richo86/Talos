using Domain.Interfaces;
using Domain.Utilities;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.DomainRepositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext context;

        public CategoryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Categorias> CreateCategory(Categorias categoria)
        {
            var codigo = (from a in context.Categorias
                          select new
                          {
                              codigo = Convert.ToInt32(a.Codigo)
                          }).ToList();
            if (codigo.Any())
                categoria.Codigo = (codigo.Max(x => x.codigo) + 1).ToString();
            else
                categoria.Codigo = 1.ToString();

            var categoriaExistente = context.Categorias.FirstOrDefault(x => x.Descripcion.Equals(categoria.Descripcion));

            if (categoriaExistente == null)
                await context.Categorias.AddAsync(categoria);
            else
                return new Categorias();

            var result = context.SaveChanges();

            if (result > 0)
                return categoria;
            else
                return new Categorias();
        }

        public async Task<Subcategorias> CreateSubCategory(Subcategorias categoria)
        {
            var codigo = (from a in context.Subcategorias
                          select new
                          {
                              codigo = Convert.ToInt32(a.Codigo)
                          }).ToList();

            if (codigo.Any())
                categoria.Codigo = (codigo.Max(x => x.codigo) + 1).ToString();
            else
                categoria.Codigo = 1.ToString();

            var categoriaExistente = context.Subcategorias.FirstOrDefault(x => x.Descripcion.Equals(categoria.Descripcion));

            if (categoriaExistente == null)
                await context.Subcategorias.AddAsync(categoria);
            else
                return new Subcategorias();

            var result = context.SaveChanges();

            if (result > 0)
                return categoria;
            else
                return new Subcategorias();
        }

        public async Task<string> DeleteCategory(string id)
        {
            var Id = Guid.Parse(id);
            var categoria = await context.Categorias.FirstOrDefaultAsync(x => x.Id.Equals(Id));
            var area = await context.Areas.FirstOrDefaultAsync(x => x.Id.Equals(Id));
            var Subcategoria = await context.Subcategorias.FirstOrDefaultAsync(x => x.Id.Equals(Id));

            if (categoria != null)
            {
                context.Categorias.Remove(categoria);

                var result = context.SaveChanges();
                if (result > 0)
                    return id;
                else
                    return "Ocurrió un error al eliminar la categoria";
            }else if(Subcategoria != null)
            {
                context.Subcategorias.Remove(Subcategoria);

                var result = context.SaveChanges();
                if (result > 0)
                    return id;
                else
                    return "Ocurrió un error al eliminar la subcategoria";
            }else if(area != null)
            {
                context.Areas.Remove(area);
                var result = context.SaveChanges();
                if (result > 0)
                    return id;
                else
                    return "Ocurrió un error al eliminar el área";
            }
            else
                return "No se encontró la categoria que se desea eliminar";
        }

        public IQueryable<CategoriaDTO> GetCategories()
        {
            var categories = (from a in context.Categorias
                              join b in context.Areas on a.Area equals b.Id into ab
                              from abResult in ab.DefaultIfEmpty()
                              where a.Descripcion != null
                              select new CategoriaDTO()
                              {
                                  Id = a.Id.ToString(),
                                  Descripcion = a.Descripcion,
                                  Codigo = a.Codigo,
                                  Area = a.Id.ToString(),
                                  AreaDescripcion = a.Descripcion,
                                  TipoCategoria = a.TipoCategoria,
                                  Imagen = a.Imagen,
                                  ImagenBase64 = a.ImagenBase64
                              });

            if (categories.Count() > 0)
                return categories;
            else
                return new List<CategoriaDTO>().AsQueryable();
        }

        public async Task<Categorias> GetCategory(string id)
        {
            var Id = Guid.Parse(id);

            var category = await context.Categorias.FirstOrDefaultAsync(x => x.Id.Equals(Id));

            if (category != null)
                return category;
            else
                return new Categorias();
        }

        public async Task<Subcategorias> GetSecondaryCategory(string id)
        {
            var Id = Guid.Parse(id);

            var category = await context.Subcategorias.FirstOrDefaultAsync(x => x.Id.Equals(Id));

            if (category != null)
                return category;
            else
                return new Subcategorias();
        }

        public async Task<List<Categorias>> GetMainCategories()
        {
            return context.Categorias.AsNoTracking().Where(x => x.TipoCategoria.Equals(Models.Enums.TipoCategoria.Principal)).ToList();
        }

        public List<Subcategorias> GetSecondaryCategories(string category)
        {
            
            if(category == "null" || category == null)
                return context.Subcategorias.AsNoTracking().Where(x => x.TipoCategoria.Equals(Models.Enums.TipoCategoria.Secundario)).ToList();
            else
            {
                var categoryId = Guid.Parse(category);
                return context.Subcategorias.AsNoTracking().Where(x => x.TipoCategoria.Equals(Models.Enums.TipoCategoria.Secundario) && x.CategoriaPrincipal.Equals(categoryId)).ToList();
            }
                
        }

        public IQueryable<CategoriaDTO> GetSubcategories()
        {
            var categories = (from a in context.Subcategorias
                              join b in context.Categorias on a.CategoriaPrincipal equals b.Id
                              where a.Descripcion != null
                              select new CategoriaDTO()
                              {
                                  Id = a.Id.ToString(),
                                  Descripcion = a.Descripcion,
                                  Codigo = a.Codigo,
                                  CategoriaPrincipal = a.CategoriaPrincipal.ToString(),
                                  CategoriaPrincipalDescripcion = b.Descripcion,
                                  TipoCategoria = a.TipoCategoria,
                                  Imagen = a.Imagen,
                                  ImagenBase64 = a.ImagenBase64
                              });

            if (categories.Count() > 0)
                return categories;
            else
                return new List<CategoriaDTO>().AsQueryable();
        }

        public async Task<Categorias> UpdateCategory(Categorias categoria)
        {
            var category = await context.Categorias.FirstOrDefaultAsync(x => x.Id.Equals(categoria.Id));

            if (category != null)
            {
                context.Entry(category).CurrentValues.SetValues(categoria);
                var result = context.SaveChanges();

                return category;
            }
            else
                return new Categorias();
        }

        public async Task<Subcategorias> UpdateSubcategory(Subcategorias categoria)
        {
            var category = await context.Subcategorias.FirstOrDefaultAsync(x => x.Id.Equals(categoria.Id));

            if (category != null)
            {
                context.Entry(category).CurrentValues.SetValues(categoria);
                var result = context.SaveChanges();

                return category;
            }
            else
                return new Subcategorias();
        }

        public async Task<Areas> GetArea(string id)
        {
            var Id = Guid.Parse(id);

            var area = await context.Areas.FirstOrDefaultAsync(x => x.Id.Equals(Id));

            if (area != null)
                return area;
            else
                return new Areas();
        }

        public IQueryable<CategoriaDTO> GetAreas()
        {
            var areas = (from a in context.Areas
                              where a.Descripcion != null
                              select new CategoriaDTO()
                              {
                                  Id = a.Id.ToString(),
                                  Descripcion = a.Descripcion,
                                  Codigo = a.Codigo,
                                  TipoCategoria = a.TipoCategoria,
                                  Imagen = a.Imagen,
                                  ImagenBase64 = a.ImagenBase64
                              });

            if (areas.Count() > 0)
                return areas;
            else
                return new List<CategoriaDTO>().AsQueryable();
        }

        public async Task<Areas> CreateArea(Areas area)
        {
            var codigo = (from a in context.Areas
                          select new
                          {
                              codigo = Convert.ToInt32(a.Codigo)
                          }).ToList();
            
            if (codigo.Any())
                area.Codigo = (codigo.Max(x => x.codigo) + 1).ToString();
            else
                area.Codigo = 1.ToString();

            var areaExistente = context.Areas.FirstOrDefault(x => x.Descripcion.Equals(area.Descripcion));

            if (areaExistente == null)
                await context.Areas.AddAsync(area);
            else
                return new Areas();

            var result = context.SaveChanges();

            if (result > 0)
                return area;
            else
                return new Areas();
        }

        public async Task<Areas> UpdateArea(Areas area)
        {
            var areaExistente = await context.Areas.FirstOrDefaultAsync(x => x.Id.Equals(area.Id));

            if (areaExistente != null)
            {
                context.Entry(areaExistente).CurrentValues.SetValues(area);
                var result = context.SaveChanges();

                return area;
            }
            else
                return new Areas();
        }

        public async Task<List<Areas>> GetMainAreas()
        {
            return context.Areas.AsNoTracking().Where(x => x.TipoCategoria.Equals(Models.Enums.TipoCategoria.Area)).ToList();
        }

        public bool CreateImage(string id, string category, string base64Image)
        {
            try
            {
                Guid categoryId = Guid.Parse(category);
                var area = context.Areas.FirstOrDefault(x => x.Id.Equals(categoryId));
                var categoria = context.Categorias.FirstOrDefault(x => x.Id.Equals(categoryId));
                var subcategoria = context.Subcategorias.FirstOrDefault(x => x.Id.Equals(categoryId));
                
                if (area != null)
                {
                    area.Imagen = id;
                    area.ImagenBase64 = base64Image;
                    context.Areas.Update(area);
                    context.SaveChanges();
                }else if (categoria != null)
                {
                    categoria.Imagen = id;
                    categoria.ImagenBase64 = base64Image;
                    context.Categorias.Update(categoria);
                    context.SaveChanges();
                }
                else if (subcategoria != null)
                {
                    subcategoria.Imagen = id;
                    subcategoria.ImagenBase64 = base64Image;
                    context.Subcategorias.Update(subcategoria);
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

        public async Task<List<CategoriaDTO>> GetCategoriesFromArea(string id)
        {
            Guid areaId = Guid.Parse(id);
            var categories = (from a in context.Categorias
                              join b in context.Areas on a.Area equals b.Id
                              where a.Area == areaId
                              select new CategoriaDTO()
                              {
                                  Id = a.Id.ToString(),
                                  Descripcion = a.Descripcion,
                                  Codigo = a.Codigo,
                                  Area = a.Area.ToString(),
                                  AreaDescripcion = b.Descripcion,
                                  Imagen = a.ImagenBase64,
                                  TipoCategoria = a.TipoCategoria
                              }).AsNoTracking().ToList();

            return categories;
        }

        public async Task<List<CategoriaDTO>> GetSubcategoriesFromCategory(string id)
        {
            Guid categoryId = Guid.Parse(id);
            var categories = (from a in context.Subcategorias
                                   join b in context.Categorias on a.CategoriaPrincipal equals b.Id
                                   where a.CategoriaPrincipal == categoryId
                                   select new CategoriaDTO()
                                   {
                                       Id = a.Id.ToString(),
                                       Descripcion = a.Descripcion,
                                       Codigo = a.Codigo,
                                       CategoriaPrincipal = a.CategoriaPrincipal.ToString(),
                                       CategoriaPrincipalDescripcion = b.Descripcion,
                                       Imagen = a.ImagenBase64,
                                       TipoCategoria = a.TipoCategoria
                                   }).AsNoTracking().ToList();

            return categories;
        }
    }
}
