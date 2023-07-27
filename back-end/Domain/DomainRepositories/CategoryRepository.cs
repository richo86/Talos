using Domain.Interfaces;
using Domain.Utilities;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Classes;
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
            var codigo = context.Categorias.Max(x => x.Codigo);
            categoria.Codigo = (Convert.ToInt32(codigo) + 1).ToString();

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
            var codigo = context.Subcategorias.Max(x => x.Codigo);
            categoria.Codigo = (Convert.ToInt32(codigo) + 1).ToString();

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
                    return "Operación exitosa";
                else
                    return "Ocurrió un error al eliminar la categoria";
            }else if(Subcategoria != null)
            {
                context.Subcategorias.Remove(Subcategoria);

                var result = context.SaveChanges();
                if (result > 0)
                    return "Operación exitosa";
                else
                    return "Ocurrió un error al eliminar la categoria";
            }else if(area != null)
            {
                context.Areas.Remove(area);
                var result = context.SaveChanges();
                if (result > 0)
                    return "Operación exitosa";
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
                                  TipoCategoria = a.TipoCategoria
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
            return await context.Categorias.AsNoTracking().Where(x => x.TipoCategoria.Equals(Models.Enums.TipoCategoria.Principal)).ToListAsync();
        }

        public async Task<List<Subcategorias>> GetSecondaryCategories()
        {
            return await context.Subcategorias.AsNoTracking().Where(x => x.TipoCategoria.Equals(Models.Enums.TipoCategoria.Secundario)).ToListAsync();
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
                                  TipoCategoria = a.TipoCategoria
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

                if (result > 0)
                    return category;
                else
                    return new Categorias();
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

                if (result > 0)
                    return category;
                else
                    return new Subcategorias();
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
                                  TipoCategoria = a.TipoCategoria
                              });

            if (areas.Count() > 0)
                return areas;
            else
                return new List<CategoriaDTO>().AsQueryable();
        }

        public async Task<Areas> CreateArea(Areas area)
        {
            var codigo = context.Areas.Max(x => x.Codigo);
            area.Codigo = (Convert.ToInt32(codigo) + 1).ToString();

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

                if (result > 0)
                    return area;
                else
                    return new Areas();
            }
            else
                return new Areas();
        }

        public async Task<List<Areas>> GetMainAreas()
        {
            return await context.Areas.AsNoTracking().Where(x => x.TipoCategoria.Equals(Models.Enums.TipoCategoria.Area)).ToListAsync();
        }

        public bool CreateImage(string id, string category)
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
                    context.Areas.Update(area);
                    context.SaveChanges();
                }else if (categoria != null)
                {
                    categoria.Imagen = id;
                    context.Categorias.Update(categoria);
                    context.SaveChanges();
                }
                else if (subcategoria != null)
                {
                    subcategoria.Imagen = id;
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
    }
}
