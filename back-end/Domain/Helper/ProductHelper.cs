using Domain.Utilities;
using Models.Classes;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Helper
{
    public class ProductHelper
    {
        private readonly ApplicationDbContext context;
        public ProductHelper(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<ProductoDTO> CreateProductDTO(List<Producto> products)
        {
            List<ProductoDTO> productList = new List<ProductoDTO>();

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
                        DescuentoId = productItem.DescuentoId != null ? productItem.DescuentoId.ToString() : null,
                        ValorDescuento = productItem.DescuentoId != null ?
                                        context.Descuentos.FirstOrDefault(x => x.Id.Equals(productItem.DescuentoId)).PorcentajeDescuento.ToString()
                                        : "0",
                        Codigo = productItem.Codigo
                    };

                    productList.Add(producto);
                }
            }

            return productList;
        }

        public List<ProductoDTO> ApplyRegionalPricing(List<ProductoDTO> products,string countryCode)
        {
            foreach (var producto in products)
            {
                var country = context.Pais.FirstOrDefault(x => x.Abreviacion.Equals(countryCode))?.Id;
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
                            products.Remove(producto);
                    }
                }

                if (producto.DescuentoId != null)
                {
                    var descuentoId = Guid.Parse(producto.DescuentoId);
                    var descuento = context.Descuentos.FirstOrDefault(x => x.Id.Equals(descuentoId)).PorcentajeDescuento;

                    producto.ValorDescuento = ((producto.Precio * (100 - descuento)) / 100).ToString();
                }
            }

            return products;
        }
    }
}
