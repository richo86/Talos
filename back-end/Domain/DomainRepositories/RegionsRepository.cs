using Domain.Interfaces;
using Domain.Utilities;
using Models.Classes;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.DomainRepositories
{
    public class RegionsRepository : IRegionsRepository
    {
        private readonly ApplicationDbContext context;

        public RegionsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> CreateRegion(List<RegionesProducto> regions)
        {
            if (!regions.Any())
                return 1;

            foreach (var item in regions)
            {
                var existing = context.RegionesProductos.FirstOrDefault(x => x.Producto.Equals(item.Producto) && x.Pais.Equals(item.Pais));
                if (existing == null)
                    await context.RegionesProductos.AddAsync(item);
            }

            var result = context.SaveChanges();

            return result;
        }

        public async Task<int> CreateRelatedProducts(List<ProductosRelacionados> products)
        {
            if (!products.Any())
                return 1;

            foreach (var item in products)
            {
                var existing = context.ProductosRelacionados.FirstOrDefault(x => x.Producto.Equals(item.Producto) && x.ProductoRelacionado.Equals(item.ProductoRelacionado));
                if (existing == null)
                    await context.ProductosRelacionados.AddAsync(item);
            }

            var result = context.SaveChanges();

            return result;
        }

        public int DeleteRegion(string id, bool save)
        {
            var productId = Guid.Parse(id);

            var regionesProducto = context.RegionesProductos.Where(x => x.Producto.Equals(productId));
            context.RemoveRange(regionesProducto);

            var productosRelacionados = context.ProductosRelacionados.Where(x => x.Producto.Equals(productId));
            context.RemoveRange(productosRelacionados);

            if (!save)
                return 1;

            var result = context.SaveChanges();

            return result;
        }

        public List<Pais> getAllCountries()
        {
            return context.Pais.ToList();
        }

        public List<RegionesProductoDTO> getAllRegions()
        {
            var products = (from a in context.RegionesProductos
                            select a.Producto).Distinct().ToList();

            List<RegionesProductoDTO> regions = new List<RegionesProductoDTO>();
            foreach (var item in products)
            {
                var producto = context.Producto.FirstOrDefault(x => x.Id.Equals(item));
                if(producto != null)
                {
                    var regionsList = (from a in context.RegionesProductos
                                   where a.Producto == item
                                   select a.Pais.ToString()).ToList();

                    var regionsDescription = (from a in context.RegionesProductos
                                              join b in context.Pais on a.Pais equals b.Id
                                              where a.Producto == item
                                              select b.Nombre).ToList();

                    var relatedProducts = (from a in context.ProductosRelacionados
                                           where a.Producto == item
                                           select a.ProductoRelacionado.ToString()).ToList();

                    var relatedProductsDescription = (from a in context.ProductosRelacionados
                                                      join b in context.Producto on a.Producto equals b.Id
                                                      where a.Producto == item
                                                      select b.Descripcion).ToList();

                    var firstRegion = context.RegionesProductos.FirstOrDefault(x => x.Producto.Equals(item));

                    RegionesProductoDTO region = new RegionesProductoDTO()
                    {
                        Producto = producto.Id.ToString(),
                        NombreProducto = producto.Nombre,
                        Regiones = regionsList,
                        RegionesDescripcion = regionsDescription,
                        ProductosRelacionados = relatedProducts,
                        ProductosRelacionadosDescripcion = relatedProductsDescription,
                        Precio = firstRegion.Precio,
                        Inventario = firstRegion.Inventario,
                        FechaCreacion = firstRegion.FechaCreacion
                    };
                    regions.Add(region);
                }
            }
            return regions;
        }

        public RegionesProductoDTO getRegionProduct(string id)
        {
            var productId = Guid.Parse(id);

            var regions = (from a in context.RegionesProductos
                            where a.Producto == productId
                            select a.Pais.ToString()).ToList();

            var regionsDescription = (from a in context.RegionesProductos
                                      join b in context.Pais on a.Pais equals b.Id
                                      where a.Producto == productId
                                      select b.Nombre).ToList();

            var relatedProducts = (from a in context.ProductosRelacionados
                            where a.Producto == productId
                            select a.ProductoRelacionado.ToString()).ToList();

            var relatedProductsDescription = (from a in context.ProductosRelacionados
                                              join b in context.Producto on a.Producto equals b.Id
                                              where a.Producto == productId
                                              select b.Descripcion).ToList();

            var firstRegion = context.RegionesProductos.FirstOrDefault(x => x.Producto.Equals(productId));
            var product = context.Producto.FirstOrDefault(x => x.Id.Equals(productId));

            if (firstRegion == null || product == null)
                return new RegionesProductoDTO();

            RegionesProductoDTO productRegion = new RegionesProductoDTO() {
                FechaCreacion = firstRegion.FechaCreacion,
                CategoriaDescripcion = context.Categorias.FirstOrDefault(x => x.Id.Equals(product.CategoriaId)).Descripcion,
                SubcategoriaDescripcion = context.Subcategorias.FirstOrDefault(x => x.Id.Equals(product.SubcategoriaId)).Descripcion,
                Producto = product.Id.ToString(),
                NombreProducto = product.Nombre,
                Regiones = regions,
                RegionesDescripcion = regionsDescription,
                ProductosRelacionados = relatedProducts,
                ProductosRelacionadosDescripcion = relatedProductsDescription,
                Precio = firstRegion.Precio,
                Inventario = firstRegion.Inventario
            };

            return productRegion;
        }
    }
}
