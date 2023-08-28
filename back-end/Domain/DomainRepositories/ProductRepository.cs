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
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext context;

        public ProductRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public bool CreateImage(string id, string product, string imageBase64)
        {
            try
            {
                Guid productId = Guid.Parse(product);
                var imagenExistente = context.Imagenes.FirstOrDefault(x => x.ImagenUrl.Equals(id) && x.ProductoId.Equals(productId));

                if(imagenExistente == null)
                {
                    Imagenes imagen = new Imagenes
                    {
                        Id = Guid.NewGuid(),
                        ProductoId = productId,
                        ImagenUrl = id,
                        ImagenBase64 = imageBase64
                    };
                    context.Imagenes.Add(imagen);
                    context.SaveChanges();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Producto> CreateProduct(Producto producto)
        {
            var categoria = await context.Categorias.FirstOrDefaultAsync(x => x.Id.Equals(producto.CategoriaId));
            if (categoria != null)
                producto.Categoria = categoria;

            var subcategoria = await context.Subcategorias.FirstOrDefaultAsync(x => x.Id.Equals(producto.SubcategoriaId));
            if (subcategoria != null)
                producto.Subcategoria = subcategoria;

            producto.FechaCreacion = DateTime.Now;

            await context.Producto.AddAsync(producto);
            var result = context.SaveChanges();

            if (result > 0)
                return producto;
            else
                return new Producto();
        }

        public async Task<string> DeleteProduct(string id)
        {
            Guid productId = Guid.Parse(id);
            var producto = await context.Producto.FirstOrDefaultAsync(x => x.Id.Equals(productId));

            if (producto != null)
            {
                context.Producto.Remove(producto);

                var imagenes = context.Imagenes.Where(x => x.ProductoId.Equals(productId));
                if(imagenes.Any())
                    context.RemoveRange(imagenes);

                var result = context.SaveChanges();
                if (result > 0)
                    return "Producto eliminado correctamente";
                else
                    return "No fue posible eliminar el producto";
            }
            else
                return "No se encontró el producto en la Base de datos";
        }

        public async Task<Producto> GetProduct(string id)
        {
            var productoId = Guid.Parse(id);
            var producto = await context.Producto.AsNoTracking()
                            .Include(x=>x.Categoria)
                            .Include(x=>x.Subcategoria)
                            .Include(x=>x.Imagenes)
                            .FirstOrDefaultAsync(x=>x.Id.Equals(productoId));

            if (producto != null)
                return producto;
            else
                return new Producto();
        }

        public List<KeyValuePair<string, string>> getProductBase64Images(string id)
        {
            Guid productId = Guid.Parse(id);

            var query = (from a in context.Imagenes
                         where a.ProductoId == productId
                         select new
                         {
                             key = a.ImagenUrl,
                             value = a.ImagenBase64 != null ? a.ImagenBase64 : a.ImagenUrl
                         });

            var images = query.AsEnumerable().Select(x => new KeyValuePair<string, string>(x.key, x.value)).ToList();

            return images;
        }

        public List<string> getProductIds(string id)
        {
            List<string> imagenes = new List<string>();
            Guid productId = Guid.Parse(id);

            var query = (from a in context.Imagenes
                         where a.ProductoId == productId
                         select new
                         {
                            id = a.ImagenUrl
                         });

            imagenes.AddRange(query.Select(x => x.id).ToList());

            return imagenes;
        }

        public IQueryable<Producto> GetProducts()
        {
            var products = context.Producto.AsNoTracking()
                            .Include(x => x.Categoria)
                            .Include(x => x.Subcategoria)
                            .Include(x => x.Imagenes.Take(1))
                            .Where(x => x.Descripcion != null);

            if (products.Count() > 0)
                return products;
            else
                return null;
        }

        public async Task<Producto> UpdateProduct(Producto producto)
        {
            var oldProduct = await context.Producto.FirstOrDefaultAsync(x=>x.Id.Equals(producto.Id));

            if (oldProduct != null)
            {
                producto.FechaModificacion = DateTime.Now;
                context.Entry(oldProduct).CurrentValues.SetValues(producto);
                var result = context.SaveChanges();
                if (result > 0)
                    return producto;
                else
                    return new Producto();
            }
            else
                return new Producto();
        }
    }
}
