using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Classes;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Talos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IRegionsRepository regionsRepository;

        public RegionsController(IMapper mapper, IRegionsRepository regionsRepository)
        {
            this.mapper = mapper;
            this.regionsRepository = regionsRepository;
        }

        [HttpGet("getRegionProduct")]
        public ActionResult<RegionesProductoDTO> getRegionProduct(string id)
        {
            try
            {
                var regionsProduct = regionsRepository.getRegionProduct(id);

                if (regionsProduct.Producto != null)
                {
                    return Ok(regionsProduct);
                }
                else
                    return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest($"Error al recuperar el producto seleccionado: {ex.Message}");
            }
        }

        [HttpGet("getAllRegions")]
        public ActionResult<List<RegionesProductoDTO>> getAllRegions()
        {
            try
            {
                var regionsProduct = regionsRepository.getAllRegions();

                if (regionsProduct.Any())
                {
                    return Ok(regionsProduct);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al recuperar las regiones: {ex.Message}");
            }
        }

        [HttpGet("getAllCountries")]
        public ActionResult<List<PaisDTO>> getAllCountries(string id)
        {
            try
            {
                var Countries = regionsRepository.getAllCountries();

                if (Countries.Any())
                {
                    var countriesDTO = mapper.Map<List<Pais>, List<PaisDTO>>(Countries);
                    return Ok(countriesDTO);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al recuperar las regiones: {ex.Message}");
            }
        }

        [HttpPost("CreateRegion")]
        public async Task<ActionResult<RegionesProducto>> CreateRegion(RegionesProductoDTO regionsDTO)
        {
            try
            {
                var errors = validateRegionProductDTO(regionsDTO);
                if (errors.Any())
                    return BadRequest(errors);

                List<RegionesProducto> regionsList = new List<RegionesProducto>();
                regionsList = buildRegions(regionsDTO);

                List<ProductosRelacionados> relatedProducts = new List<ProductosRelacionados>();
                relatedProducts = buildRelatedProducts(regionsDTO.ProductosRelacionados, regionsDTO.Producto);

                var result = await regionsRepository.CreateRegion(regionsList);
                var related = await regionsRepository.CreateRelatedProducts(relatedProducts);

                if (result > 0 && related > 0)
                {
                    return Ok();
                }
                else
                    return BadRequest("No fue posible insertar las regiones. Por favor intentelo más tarde");
            }
            catch(Exception ex)
            {
                return BadRequest($"Error al crear las regiones: {ex.Message}");
            }
        }

        [HttpPut("UpdateRegion")]
        public async Task<ActionResult<RegionesProducto>> UpdateRegion(RegionesProductoDTO regionsDTO)
        {
            try
            {
                var errors = validateRegionProductDTO(regionsDTO);
                if (errors.Any())
                    return BadRequest(errors);

                var delete = regionsRepository.DeleteRegion(regionsDTO.Producto,false);

                if(delete > 0)
                {
                    List<RegionesProducto> regionsList = new List<RegionesProducto>();
                    regionsList = buildRegions(regionsDTO);

                    List<ProductosRelacionados> relatedProducts = new List<ProductosRelacionados>();
                    relatedProducts = buildRelatedProducts(regionsDTO.ProductosRelacionados, regionsDTO.Producto);

                    var result = await regionsRepository.CreateRegion(regionsList);
                    var related = await regionsRepository.CreateRelatedProducts(relatedProducts);

                    if (result > 0 && related > 0)
                    {
                        return Ok();
                    }
                    else
                        return BadRequest("No fue posible actualizar las regiones. Por favor intentelo más tarde");
                }
                else
                    return BadRequest("No fue posible actualizar las regiones. Por favor intentelo más tarde");

            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar las regiones: {ex.Message}");
            }
        }

        [HttpDelete("DeleteRegion")]
        public ActionResult<RegionesProducto> DeleteRegion(string id)
        {
            try
            {
                var result = regionsRepository.DeleteRegion(id,true);

                if (result > 0)
                {
                    return Ok(result);
                }
                else
                    return BadRequest("No fue posible eliminar las regiones. Por favor intentelo más tarde");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar las regiones: {ex.Message}");
            }
        }

        #region

        private List<string> validateRegionProductDTO(RegionesProductoDTO regionProduct)
        {
            List<string> errorList = new List<string>();

            if (regionProduct.Producto == null)
                errorList.Add("Es necesario incluir el Id del producto");

            if (!regionProduct.ProductosRelacionados.Any())
                errorList.Add("Es necesario incluir al menos una region");

            return errorList;
        }

        private List<RegionesProducto> buildRegions(RegionesProductoDTO regionsDTO)
        {
            List<RegionesProducto> regionsList = new List<RegionesProducto>();
            foreach (var item in regionsDTO.Regiones)
            {
                RegionesProducto RegionesProductoBase = new RegionesProducto()
                {
                    Producto = Guid.Parse(regionsDTO.Producto),
                    Precio = regionsDTO.Precio,
                    Inventario = regionsDTO.Inventario,
                    FechaCreacion = DateTime.Now
                };

                RegionesProductoBase.Id = Guid.NewGuid();
                RegionesProductoBase.Pais = Guid.Parse(item);
                regionsList.Add(RegionesProductoBase);
            }

            return regionsList;
        }

        private List<ProductosRelacionados> buildRelatedProducts(List<string> related, string Producto)
        {
            List<ProductosRelacionados> relatedProducts = new List<ProductosRelacionados>();
            foreach (var item in related)
            {
                ProductosRelacionados newRelatedProduct = new ProductosRelacionados();
                newRelatedProduct.ID = Guid.NewGuid();
                newRelatedProduct.Producto = Guid.Parse(Producto);
                newRelatedProduct.ProductoRelacionado = Guid.Parse(item);

                relatedProducts.Add(newRelatedProduct);
            }

            return relatedProducts;
        }

        #endregion
    }
}
