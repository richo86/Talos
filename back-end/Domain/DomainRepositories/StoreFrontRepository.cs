using Domain.Helper;
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
    public class StoreFrontRepository : IStoreFrontRepository
    {
        private readonly ApplicationDbContext context;
        private readonly ProductHelper productHelper;

        public StoreFrontRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.productHelper = new ProductHelper(this.context);
        }

        public CategoriasProductoDTO GetAllMenuItems()
        {
            CategoriasProductoDTO listadoCategoriasProducto = new CategoriasProductoDTO();
            listadoCategoriasProducto.Areas = new List<AreasDTO>();

            var products = (from a in context.Producto
                            join b in context.ItemsPedido on a.Id equals b.ProductoId into ab
                            from abResult in ab.DefaultIfEmpty()
                            join c in context.DetallePedidos on abResult.DetallePedidosId equals c.Id into bc
                            from bcResult in bc.DefaultIfEmpty()
                            join d in context.Pagos on bcResult.PagoId equals d.Id into cd
                            from cdResult in cd.DefaultIfEmpty()
                            select new { Id = a.Id }).AsNoTracking().GroupBy(p => p.Id).Select(y => new { Id = y.Key, cantidad = y.Count() }).OrderByDescending(x => x.cantidad).ToList();

            var areas = new Dictionary<Guid, string>();
            if (products.Any())
            {
                foreach (var item in products)
                {
                    var area = (from a in context.Producto
                                join b in context.Categorias on a.CategoriaId equals b.Id
                                join c in context.Areas on b.Area equals c.Id
                                select new
                                {
                                    Id = c.Id,
                                    Descripcion = c.Descripcion
                                }).AsNoTracking().FirstOrDefault();
                    areas.Add(area.Id, area.Descripcion);
                }
            }
            else
            {
                var allAreas = context.Areas.Where(x=>x.Descripcion != null).Select(x=>new { Id = x.Id, Descripcion = x.Descripcion });
                foreach (var item in allAreas)
                {
                    areas.Add(item.Id, item.Descripcion);
                }
            }
            
            var categorias = context.Categorias.AsNoTracking().Where(x => x.Descripcion != null).ToList();
            var subcategorias = context.Subcategorias.AsNoTracking().Where(x => x.Descripcion != null).ToList();

            foreach (var area in areas)
            {
                AreasDTO nuevaArea = new AreasDTO()
                {
                    Id = area.Key.ToString(),
                    Descripcion = area.Value
                };
                List<CategoriaPrincipalDTO> categoriasPrincipales = new List<CategoriaPrincipalDTO>();
                var listadoCategorias = categorias.Where(x => x.Area.Equals(area.Key));
                foreach (var categoria in listadoCategorias)
                {
                    CategoriaPrincipalDTO nuevaPrincipal = new CategoriaPrincipalDTO()
                    {
                        Id = categoria.Id.ToString(),
                        Descripcion = categoria.Descripcion,
                        Codigo = categoria.Codigo
                    };

                    categoriasPrincipales.Add(nuevaPrincipal);
                }
                nuevaArea.Categorias = categoriasPrincipales;
                listadoCategoriasProducto.Areas.Add(nuevaArea);
            }

            return listadoCategoriasProducto;
        }

        public List<ProductoDTO> GetAllProducts(string countryCode)
        {
            List<ProductoDTO> productList = new List<ProductoDTO>();

            var products = (from a in context.Producto
                         join b in context.ItemsPedido on a.Id equals b.ProductoId into ab
                         from abResult in ab.DefaultIfEmpty()
                         join c in context.DetallePedidos on abResult.DetallePedidosId equals c.Id into bc
                         from bcResult in bc.DefaultIfEmpty()
                         join d in context.Pagos on bcResult.PagoId equals d.Id into cd
                         from cdResult in cd.DefaultIfEmpty()
                         select new { a.Id }).AsNoTracking().GroupBy(p => p.Id).Select(y => new { Id = y.Key, cantidad = y.Count() }).OrderByDescending(x=>x.cantidad).ToList();

            productList = productHelper.CreateProductDTOFromIds(products.Select(x=>x.Id).ToList());

            productList = productHelper.ApplyRegionalPricing(productList, countryCode);

            return productList;
        }

        public List<ProductoDTO> GetBestSellers(string countryCode)
        {
            List<ProductoDTO> productList = new List<ProductoDTO>();

            var sales = (from a in context.Pagos
                         join b in context.DetallePedidos on a.Id equals b.PagoId
                         join c in context.ItemsPedido on b.Id equals c.DetallePedidosId
                         join d in context.Producto on c.ProductoId equals d.Id
                         select new { d.Id }).GroupBy(x=>x.Id).Select(y => new { Id = y.Key, cantidad = y.Count() }).OrderByDescending(z => z.cantidad).Take(12).ToList();

            productList = productHelper.CreateProductDTOFromIds(sales.Select(x => x.Id).ToList());

            productList = productHelper.ApplyRegionalPricing(productList, countryCode);

            return productList;
        }

        public List<ProductoDTO> GetDiscountedProducts(string countryCode)
        {
            List<ProductoDTO> productList = new List<ProductoDTO>();

            var products = (from a in context.Producto
                            join b in context.Descuentos on a.DescuentoId equals b.Id
                            where a.DescuentoId != null
                            select a).OrderBy(x => x.Precio).ToList();

            productList = productHelper.CreateProductDTO(products);

            productList = productHelper.ApplyRegionalPricing(productList,countryCode);

            return productList;
        }

        public List<ProductoDTO> GetLatestProducts(string countryCode)
        {
            List<ProductoDTO> productList = new List<ProductoDTO>();

            var products = (from a in context.Producto
                            where a.Inventario > 0
                            select a).OrderByDescending(x=>x.FechaCreacion).Take(12).ToList();

            productList = productHelper.CreateProductDTO(products);

            productList = productHelper.ApplyRegionalPricing(productList, countryCode);

            return productList;
        }

        public List<ProductoDTO> GetLowestCost(string countryCode)
        {
            List<ProductoDTO> productList = new List<ProductoDTO>();

            var products = (from a in context.Producto
                            where a.Inventario > 0
                            select a).OrderBy(x => x.Precio).Take(12).ToList();

            productList = productHelper.CreateProductDTO(products);

            productList = productHelper.ApplyRegionalPricing(productList, countryCode);

            return productList;
        }

        public List<ProductoDTO> GetProductsFromArea(string countryCode)
        {
            throw new NotImplementedException();
        }

        public List<ProductoDTO> GetProductsFromCategory(string countryCode)
        {
            throw new NotImplementedException();
        }

        public List<ProductoDTO> GetProductsFromSpecificArea(string countryCode, string area)
        {
            List<ProductoDTO> productList = new List<ProductoDTO>();

            var products = (from a in context.Categorias
                            join b in context.Areas on a.Area equals b.Id
                            join c in context.Producto on a.Id equals c.CategoriaId
                            where b.Id == Guid.Parse(area)
                            select c).OrderByDescending(x => x.FechaCreacion).ToList();

            productList = productHelper.CreateProductDTO(products);

            productList = productHelper.ApplyRegionalPricing(productList, countryCode);

            return productList;
        }

        public List<ProductoDTO> GetProductsFromSpecificCategory(string countryCode, string category)
        {
            List<ProductoDTO> productList = new List<ProductoDTO>();

            var products = (from a in context.Categorias
                            join b in context.Producto on a.Id equals b.CategoriaId
                            where a.Id == Guid.Parse(category)
                            select b).OrderByDescending(x => x.FechaCreacion).ToList();

            productList = productHelper.CreateProductDTO(products);

            productList = productHelper.ApplyRegionalPricing(productList, countryCode);

            return productList;
        }

        public List<ProductoDTO> GetProductsFromSpecificSubcategory(string countryCode, string subcategory)
        {
            List<ProductoDTO> productList = new List<ProductoDTO>();

            var products = (from a in context.Subcategorias
                            join b in context.Producto on a.Id equals b.SubcategoriaId
                            where a.Id == Guid.Parse(subcategory)
                            select b).OrderByDescending(x => x.FechaCreacion).ToList();

            productList = productHelper.CreateProductDTO(products);

            productList = productHelper.ApplyRegionalPricing(productList, countryCode);

            return productList;
        }

        public List<ProductoDTO> GetProductsFromSubcategory(string countryCode)
        {
            throw new NotImplementedException();
        }

        public List<CollectionDTO> GetStoreAreas()
        {
            return context.Areas.AsNoTracking().Where(x => x.Descripcion != null).Select(x=>new CollectionDTO
            {
                Id = x.Id.ToString(),
                Image = x.ImagenBase64,
                Name = x.Descripcion,
                Route = $"/areas/{x.Id}"
            }).ToList();
        }

        public List<CollectionDTO> GetStoreCategories()
        {
            return context.Categorias.AsNoTracking().Where(x => x.Descripcion != null).Select(x => new CollectionDTO
            {
                Id = x.Id.ToString(),
                Image = x.ImagenBase64,
                Name = x.Descripcion,
                Route = $"/categories/{x.Id}"
            }).ToList();
        }

        public List<CollectionDTO> GetStoreSubcategories()
        {
            return context.Subcategorias.AsNoTracking().Where(x => x.Descripcion != null).Select(x => new CollectionDTO
            {
                Id = x.Id.ToString(),
                Image = x.ImagenBase64,
                Name = x.Descripcion,
                Route = $"/subcategories/{x.Id}"
            }).ToList();
        }

        public List<CollectionDTO> GetTopAreas(string countryCode)
        {
            var categories = (from a in context.Areas
                              join b in context.Categorias on a.Id equals b.Area
                              join c in context.Producto on b.Id equals c.CategoriaId
                              where a.Descripcion != null
                              select new CollectionDTO
                              {
                                  Id = a.Id.ToString(),
                                  Image = a.Imagen,
                                  Name = a.Descripcion,
                                  Route = "/areas/" + a.Id,
                                  Price = c.Precio
                              }).OrderBy(x => x.Price).Take(6).ToList();

            if(categories.Count() < 3)
            {
                categories = (from b in context.Categorias
                        join c in context.Producto on b.Id equals c.CategoriaId
                        where b.Descripcion != null
                        select new CollectionDTO
                        {
                            Id = b.Id.ToString(),
                            Image = b.Imagen,
                            Name = b.Descripcion,
                            Route = "/categories/" + b.Id,
                            Price = c.Precio
                        }).OrderBy(x => x.Price).Take(6).ToList();
            }

            return categories;
    }

        public List<CollectionDTO> GetTopSubcategories(string countryCode)
        {
            return (from a in context.Subcategorias
                    join c in context.Producto on a.Id equals c.SubcategoriaId
                    select new CollectionDTO
                    {
                        Id = a.Id.ToString(),
                        Image = a.Imagen,
                        Name = a.Descripcion,
                        Route = "/subcategories/" + a.Id,
                        Price = c.Precio
                    }).OrderBy(x => x.Price).Take(2).ToList();
        }

        public async Task<List<SearchResults>> SearchItems(string search, string countryCode)
        {
            List<SearchResults> results = new List<SearchResults>();

            results = PredefinedResults(search);

            var products = context.Producto.Where(x => EF.Functions.Like(x.Nombre, $"%{search}%") || EF.Functions.Like(x.Descripcion, $"%{search}%")).ToList();
            results.AddRange(products.Select(x => new SearchResults()
            {
                Description = x.Nombre.ToString(),
                Link = $"/product-page/{x.Id}"
            }));

            var areas = context.Areas.Where(x => EF.Functions.Like(x.Descripcion, $"%{search}%")).ToList();
            results.AddRange(areas.Select(x => new SearchResults()
            {
                Description = x.Descripcion.ToString(),
                Link = $"/areas/{x.Id}"
            }));

            var categories = context.Categorias.Where(x => EF.Functions.Like(x.Descripcion, $"%{search}%")).ToList();
            results.AddRange(categories.Select(x => new SearchResults()
            {
                Description = x.Descripcion.ToString(),
                Link = $"/categories/{x.Id}"
            }));

            var subcategories = context.Subcategorias.Where(x => EF.Functions.Like(x.Descripcion, $"%{search}%")).ToList();
            results.AddRange(subcategories.Select(x => new SearchResults()
            {
                Description = x.Descripcion.ToString(),
                Link = $"/subcategories/{x.Id}"
            }));

            return results;
        }

        private List<SearchResults> PredefinedResults(string search)
        {
            List<SearchResults> results = new List<SearchResults>();
            List<string> predefinedBestSellers = new List<string>()
            {
                "best",
                "mejor",
                "mejores",
            };

            if (predefinedBestSellers.Contains(search))
            {
                results.Add(new SearchResults()
                {
                    Description = "Best sellers",
                    Link = $"/best-sellers"
                });
            }

            List<string> predefinedAllItems = new List<string>()
            {
                "all",
                "todos",
            };
            if (predefinedAllItems.Contains(search))
            {
                results.Add(new SearchResults()
                {
                    Description = "All products",
                    Link = "/all-products"
                });
            }

            return results;
        }
    }
}
