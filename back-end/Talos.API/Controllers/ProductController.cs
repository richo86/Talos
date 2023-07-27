using AutoMapper;
using Domain.Interfaces;
using Domain.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Classes;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talos.API.User;

namespace Talos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly IProductRepository productRepository;
        private readonly IDriveRepository driveRepository;

        public ProductController(UserManager<ApplicationUser> userManager, IMapper mapper, IProductRepository productRepository, IDriveRepository driveRepository)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.productRepository = productRepository;
            this.driveRepository = driveRepository;
        }

        [HttpGet("getProduct")]
        public async Task<ActionResult<ProductoDTO>> GetProduct(string id)
        {
            try
            {
                var product = await productRepository.GetProduct(id);

                if (product.Descripcion != null)
                {
                    var productDTO = mapper.Map<Producto, ProductoDTO>(product);
                    //productDTO.ImagenesBase64 = driveRepository.GetFilesByIds(productDTO.Imagenes);
                    return Ok(productDTO);
                }
                else
                    return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest($"Error al recuperar el producto seleccionado: {ex.Message}");
            }
        }

        [HttpGet("getAllProducts")]
        public async Task<ActionResult<List<ProductoDTO>>> GetAllProducts([FromQuery] PaginacionDTO paginacionDTO)
        {
            try
            {
                var queryable = productRepository.GetProducts();
                if (queryable == null)
                    return NotFound();

                await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
                var products = await queryable.OrderBy(x => x.FechaCreacion).Paginar(paginacionDTO).ToListAsync();

                if (products.Any())
                {
                    var productsDTO = mapper.Map<List<Producto>, List<ProductoDTO>>(products);

                    foreach (var item in productsDTO)
                    {
                        item.Imagenes[0] = driveRepository.GetFileById(item.Imagenes.FirstOrDefault());
                    }

                    return Ok(productsDTO);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al recuperar los productos: {ex.Message}");
            }
        }

        [HttpPost("CreateProduct")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult<Producto>> CreateProduct(CrearProductoDTO producto)
        {
            try
            {
                var validation = validateProductDTO(producto);
                if (validation.Count() > 0)
                    return BadRequest(validation);

                var newProduct = mapper.Map<CrearProductoDTO, Producto>(producto);

                var product = await productRepository.CreateProduct(newProduct);

                if (product.Descripcion != null)
                {
                    var productDTO = mapper.Map<Producto, ProductoDTO>(product);
                    return Ok(productDTO);
                }
                else
                    return BadRequest("No fue posible crear el producto");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al recuperar los productos: {ex.Message}");
            }
        }

        [HttpPut("UpdateProduct")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult<ProductoDTO>> UpdateProduct([FromForm] CrearProductoDTO producto)
        {
            try
            {
                var validation = validateProductDTO(producto);
                if (validation.Count() > 0)
                    return BadRequest(validation);

                var updateProduct = mapper.Map<CrearProductoDTO, Producto>(producto);

                var product = await productRepository.UpdateProduct(updateProduct);

                if (product.Descripcion != null)
                {
                    var productDTO = mapper.Map<Producto, ProductoDTO>(product);
                    return Ok(productDTO);
                }
                else
                    return BadRequest("No fue posible actualizar el producto");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el producto: {ex.Message}");
            }
        }

        [HttpDelete("DeleteProduct")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult<string>> DeleteProduct(string id)
        {
            try
            {
                var result = await productRepository.DeleteProduct(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar el producto: {ex.Message}");
            }
        }

        [HttpGet("getProductIds")]
        public ActionResult<ProductoDTO> getProductIds(string id)
        {
            try
            {
                var images = productRepository.getProductIds(id);

                if (images.Any())
                    return Ok(images);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al recuperar el listado de IDs: {ex.Message}");
            }
        }

        #region

        private List<string> validateProductDTO(CrearProductoDTO product)
        {
            List<string> errorList = new List<string>();

            if (product.CategoriaId == null)
                errorList.Add("Es necesario incluir la categoria del producto");

            if (product.SubcategoriaId == null)
                errorList.Add("Es necesario incluir la subcategoria del producto");

            if (product.Descripcion == null)
                errorList.Add("Es necesario incluir la descripción del producto");

            if (product.Nombre == null)
                errorList.Add("Es necesario incluir el nombre del producto");

            if (product.Precio == 0)
                errorList.Add("Es necesario incluir el precio del producto");

            return errorList;
        }

        #endregion
    }
}
