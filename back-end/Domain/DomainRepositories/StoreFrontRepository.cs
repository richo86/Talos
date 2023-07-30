using Domain.Interfaces;
using Domain.Utilities;
using Microsoft.EntityFrameworkCore;
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

        public StoreFrontRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<CategoriasProductoDTO> GetAllMenuItems()
        {
            CategoriasProductoDTO listadoCategoriasProducto = new CategoriasProductoDTO();
            listadoCategoriasProducto.Areas = new List<AreasDTO>();

            var areas = await context.Areas.Where(x => x.Descripcion != null).ToListAsync();
            var categorias = await context.Categorias.Where(x => x.Descripcion != null).ToListAsync();
            var subcategorias = await context.Subcategorias.Where(x => x.Descripcion != null).ToListAsync();

            foreach (var area in areas)
            {
                AreasDTO nuevaArea = new AreasDTO()
                {
                    Id = area.Id.ToString(),
                    Descripcion = area.Descripcion
                };
                List<CategoriaPrincipalDTO> categoriasPrincipales = new List<CategoriaPrincipalDTO>();
                var listadoCategorias = categorias.Where(x => x.Area.Equals(area.Id));
                foreach (var categoria in listadoCategorias)
                {
                    CategoriaPrincipalDTO nuevaPrincipal = new CategoriaPrincipalDTO()
                    {
                        Id = categoria.Id.ToString(),
                        Descripcion = categoria.Descripcion,
                        Codigo = categoria.Codigo
                    };

                    List<CategoriaSecundariaDTO> Subcategorias = new List<CategoriaSecundariaDTO>();
                    var listadoSubcategorias = subcategorias.Where(x => x.CategoriaPrincipal.Equals(categoria.Id));
                    foreach (var subcategoria in listadoSubcategorias)
                    {
                        CategoriaSecundariaDTO nuevaSecundaria = new CategoriaSecundariaDTO()
                        {
                            Id = subcategoria.Id.ToString(),
                            Descripcion = subcategoria.Descripcion,
                            Codigo = subcategoria.Codigo
                        };
                        Subcategorias.Add(nuevaSecundaria);
                    }
                    nuevaPrincipal.Subcategorias = Subcategorias;
                    categoriasPrincipales.Add(nuevaPrincipal);
                }
                nuevaArea.Categorias = categoriasPrincipales;
                listadoCategoriasProducto.Areas.Add(nuevaArea);
            }

            return listadoCategoriasProducto;
        }

        public List<ProductoDTO> GetBestSellers(string countryCode)
        {
            List<ProductoDTO> productList = new List<ProductoDTO>();

            var sales = (from a in context.Pagos
                         join b in context.DetallePedidos on a.Id equals b.PagoId
                         join c in context.ItemsPedido on b.Id equals c.DetallePedidosId
                         join d in context.Producto on c.ProductoId equals d.Id
                         select new { d.Id, d.Descripcion }).GroupBy(x=>x.Id).Select(y=> new { Id = y.Key, cantidad = y.Count() }).Take(12);

            foreach (var item in sales)
            {
                var productItem = context.Producto.FirstOrDefault(x => x.Id.Equals(item.Id));
                if(productItem != null)
                {
                    var images = context.Imagenes.Where(x => x.ProductoId.Equals(item.Id)).Select(x=>x.ImagenUrl).ToList();
                    ProductoDTO producto = new ProductoDTO()
                    {
                        Id = productItem.Id.ToString(),
                        Nombre = productItem.Nombre,
                        Descripcion = productItem.Descripcion,
                        Inventario = productItem.Inventario.ToString(),
                        Precio = productItem.Precio,
                        Imagenes = images,
                        FechaCreacion = productItem.FechaCreacion,
                        FechaModificacion = productItem.FechaModificacion,
                        CategoriaId = productItem.CategoriaId.ToString(),
                        CategoriaDescripcion = context.Categorias.FirstOrDefault(x=>x.Id.Equals(productItem.CategoriaId)).Descripcion,
                        SubcategoriaId = productItem.SubcategoriaId.ToString(),
                        SubcategoriaDescripcion = context.Subcategorias.FirstOrDefault(x=>x.Id.Equals(productItem.SubcategoriaId)).Descripcion,
                        DescuentoId = productItem.DescuentoId.ToString(),
                        ValorDescuento = context.Descuentos.FirstOrDefault(x=>x.Id.Equals(productItem.DescuentoId)).PorcentajeDescuento.ToString(),
                        Codigo = productItem.Codigo
                    };

                    var country = context.Pais.FirstOrDefault(x => x.Abreviacion.Equals(countryCode)).Id;
                    var region = context.RegionesProductos.FirstOrDefault(x => x.Pais.Equals(country));
                    if(region != null)
                    {
                        if (region.Inventario > -1)
                            producto.Inventario = region.Inventario.ToString();

                        if(region.Precio != null)
                            producto.Precio = region.Precio.Value;
                    }
                }
            }

            return productList;
        }

        public List<ProductoDTO> GetLatestProducts(string countryCode)
        {
            List<ProductoDTO> productList = new List<ProductoDTO>();

            var products = (from a in context.Producto
                            where a.Inventario > 0
                            select a).OrderByDescending(x=>x.FechaCreacion).Take(12).ToList();

            foreach (var item in products)
            {
                var productItem = context.Producto.FirstOrDefault(x => x.Id.Equals(item.Id));
                if (productItem != null)
                {
                    var images = context.Imagenes.Where(x => x.ProductoId.Equals(item.Id)).Select(x => x.ImagenUrl).ToList();
                    ProductoDTO producto = new ProductoDTO()
                    {
                        Id = productItem.Id.ToString(),
                        Nombre = productItem.Nombre,
                        Descripcion = productItem.Descripcion,
                        Inventario = productItem.Inventario.ToString(),
                        Precio = productItem.Precio,
                        Imagenes = images,
                        FechaCreacion = productItem.FechaCreacion,
                        FechaModificacion = productItem.FechaModificacion,
                        CategoriaId = productItem.CategoriaId.ToString(),
                        CategoriaDescripcion = context.Categorias.FirstOrDefault(x => x.Id.Equals(productItem.CategoriaId)).Descripcion,
                        SubcategoriaId = productItem.SubcategoriaId.ToString(),
                        SubcategoriaDescripcion = context.Subcategorias.FirstOrDefault(x => x.Id.Equals(productItem.SubcategoriaId)).Descripcion,
                        DescuentoId = productItem.DescuentoId.ToString(),
                        ValorDescuento = context.Descuentos.FirstOrDefault(x => x.Id.Equals(productItem.DescuentoId)).PorcentajeDescuento.ToString(),
                        Codigo = productItem.Codigo
                    };

                    var country = context.Pais.FirstOrDefault(x => x.Abreviacion.Equals(countryCode)).Id;
                    var region = context.RegionesProductos.FirstOrDefault(x => x.Pais.Equals(country));
                    if (region != null)
                    {
                        if (region.Inventario > -1)
                            producto.Inventario = region.Inventario.ToString();

                        if (region.Precio != null)
                            producto.Precio = region.Precio.Value;

                        var productHasOtherRegion = context.RegionesProductos.Where(x => x.Producto.Equals(producto.Id) && x.Pais != region.Id);
                        if (productHasOtherRegion.Any())
                        {
                            var productInCountry = context.RegionesProductos.FirstOrDefault(x => x.Producto.Equals(producto.Id) && x.Pais.Equals(region.Id));
                            if(productInCountry == null)
                                products.Remove(item);
                        }
                    }
                }
            }

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

            foreach (var item in products)
            {
                var productItem = context.Producto.FirstOrDefault(x => x.Id.Equals(item.Id));
                if (productItem != null)
                {
                    var images = context.Imagenes.Where(x => x.ProductoId.Equals(item.Id)).Select(x => x.ImagenUrl).ToList();
                    ProductoDTO producto = new ProductoDTO()
                    {
                        Id = productItem.Id.ToString(),
                        Nombre = productItem.Nombre,
                        Descripcion = productItem.Descripcion,
                        Inventario = productItem.Inventario.ToString(),
                        Precio = productItem.Precio,
                        Imagenes = images,
                        FechaCreacion = productItem.FechaCreacion,
                        FechaModificacion = productItem.FechaModificacion,
                        CategoriaId = productItem.CategoriaId.ToString(),
                        CategoriaDescripcion = context.Categorias.FirstOrDefault(x => x.Id.Equals(productItem.CategoriaId)).Descripcion,
                        SubcategoriaId = productItem.SubcategoriaId.ToString(),
                        SubcategoriaDescripcion = context.Subcategorias.FirstOrDefault(x => x.Id.Equals(productItem.SubcategoriaId)).Descripcion,
                        DescuentoId = productItem.DescuentoId.ToString(),
                        ValorDescuento = context.Descuentos.FirstOrDefault(x => x.Id.Equals(productItem.DescuentoId)).PorcentajeDescuento.ToString(),
                        Codigo = productItem.Codigo
                    };

                    var country = context.Pais.FirstOrDefault(x => x.Abreviacion.Equals(countryCode)).Id;
                    var region = context.RegionesProductos.FirstOrDefault(x => x.Pais.Equals(country));
                    if (region != null)
                    {
                        if (region.Inventario > -1)
                            producto.Inventario = region.Inventario.ToString();

                        if (region.Precio != null)
                            producto.Precio = region.Precio.Value;

                        var productHasOtherRegion = context.RegionesProductos.Where(x => x.Producto.Equals(producto.Id) && x.Pais != region.Id);
                        if (productHasOtherRegion.Any())
                        {
                            var productInCountry = context.RegionesProductos.FirstOrDefault(x => x.Producto.Equals(producto.Id) && x.Pais.Equals(region.Id));
                            if (productInCountry == null)
                                products.Remove(item);
                        }
                    }
                }
            }

            return productList;
        }

        public List<ProductoDTO> GetProductsFromSpecificCategory(string countryCode, string category)
        {
            List<ProductoDTO> productList = new List<ProductoDTO>();

            var products = (from a in context.Categorias
                            join b in context.Producto on a.Id equals b.CategoriaId
                            where a.Id == Guid.Parse(category)
                            select b).OrderByDescending(x => x.FechaCreacion).ToList();

            foreach (var item in products)
            {
                var productItem = context.Producto.FirstOrDefault(x => x.Id.Equals(item.Id));
                if (productItem != null)
                {
                    var images = context.Imagenes.Where(x => x.ProductoId.Equals(item.Id)).Select(x => x.ImagenUrl).ToList();
                    ProductoDTO producto = new ProductoDTO()
                    {
                        Id = productItem.Id.ToString(),
                        Nombre = productItem.Nombre,
                        Descripcion = productItem.Descripcion,
                        Inventario = productItem.Inventario.ToString(),
                        Precio = productItem.Precio,
                        Imagenes = images,
                        FechaCreacion = productItem.FechaCreacion,
                        FechaModificacion = productItem.FechaModificacion,
                        CategoriaId = productItem.CategoriaId.ToString(),
                        CategoriaDescripcion = context.Categorias.FirstOrDefault(x => x.Id.Equals(productItem.CategoriaId)).Descripcion,
                        SubcategoriaId = productItem.SubcategoriaId.ToString(),
                        SubcategoriaDescripcion = context.Subcategorias.FirstOrDefault(x => x.Id.Equals(productItem.SubcategoriaId)).Descripcion,
                        DescuentoId = productItem.DescuentoId.ToString(),
                        ValorDescuento = context.Descuentos.FirstOrDefault(x => x.Id.Equals(productItem.DescuentoId)).PorcentajeDescuento.ToString(),
                        Codigo = productItem.Codigo
                    };

                    var country = context.Pais.FirstOrDefault(x => x.Abreviacion.Equals(countryCode)).Id;
                    var region = context.RegionesProductos.FirstOrDefault(x => x.Pais.Equals(country));
                    if (region != null)
                    {
                        if (region.Inventario > -1)
                            producto.Inventario = region.Inventario.ToString();

                        if (region.Precio != null)
                            producto.Precio = region.Precio.Value;

                        var productHasOtherRegion = context.RegionesProductos.Where(x => x.Producto.Equals(producto.Id) && x.Pais != region.Id);
                        if (productHasOtherRegion.Any())
                        {
                            var productInCountry = context.RegionesProductos.FirstOrDefault(x => x.Producto.Equals(producto.Id) && x.Pais.Equals(region.Id));
                            if (productInCountry == null)
                                products.Remove(item);
                        }
                    }
                }
            }

            return productList;
        }

        public List<ProductoDTO> GetProductsFromSpecificSubcategory(string countryCode, string subcategory)
        {
            List<ProductoDTO> productList = new List<ProductoDTO>();

            var products = (from a in context.Subcategorias
                            join b in context.Producto on a.Id equals b.SubcategoriaId
                            where a.Id == Guid.Parse(subcategory)
                            select b).OrderByDescending(x => x.FechaCreacion).ToList();

            foreach (var item in products)
            {
                var productItem = context.Producto.FirstOrDefault(x => x.Id.Equals(item.Id));
                if (productItem != null)
                {
                    var images = context.Imagenes.Where(x => x.ProductoId.Equals(item.Id)).Select(x => x.ImagenUrl).ToList();
                    ProductoDTO producto = new ProductoDTO()
                    {
                        Id = productItem.Id.ToString(),
                        Nombre = productItem.Nombre,
                        Descripcion = productItem.Descripcion,
                        Inventario = productItem.Inventario.ToString(),
                        Precio = productItem.Precio,
                        Imagenes = images,
                        FechaCreacion = productItem.FechaCreacion,
                        FechaModificacion = productItem.FechaModificacion,
                        CategoriaId = productItem.CategoriaId.ToString(),
                        CategoriaDescripcion = context.Categorias.FirstOrDefault(x => x.Id.Equals(productItem.CategoriaId)).Descripcion,
                        SubcategoriaId = productItem.SubcategoriaId.ToString(),
                        SubcategoriaDescripcion = context.Subcategorias.FirstOrDefault(x => x.Id.Equals(productItem.SubcategoriaId)).Descripcion,
                        DescuentoId = productItem.DescuentoId.ToString(),
                        ValorDescuento = context.Descuentos.FirstOrDefault(x => x.Id.Equals(productItem.DescuentoId)).PorcentajeDescuento.ToString(),
                        Codigo = productItem.Codigo
                    };

                    var country = context.Pais.FirstOrDefault(x => x.Abreviacion.Equals(countryCode)).Id;
                    var region = context.RegionesProductos.FirstOrDefault(x => x.Pais.Equals(country));
                    if (region != null)
                    {
                        if (region.Inventario > -1)
                            producto.Inventario = region.Inventario.ToString();

                        if (region.Precio != null)
                            producto.Precio = region.Precio.Value;

                        var productHasOtherRegion = context.RegionesProductos.Where(x => x.Producto.Equals(producto.Id) && x.Pais != region.Id);
                        if (productHasOtherRegion.Any())
                        {
                            var productInCountry = context.RegionesProductos.FirstOrDefault(x => x.Producto.Equals(producto.Id) && x.Pais.Equals(region.Id));
                            if (productInCountry == null)
                                products.Remove(item);
                        }
                    }
                }
            }

            return productList;
        }

        public List<ProductoDTO> GetProductsFromSubcategory(string countryCode)
        {
            throw new NotImplementedException();
        }
    }
}
