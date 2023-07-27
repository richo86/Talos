using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    public class CartController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly ICartRepository cartRepository;

        public CartController(UserManager<ApplicationUser> userManager, IMapper mapper, ICartRepository cartRepository)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.cartRepository = cartRepository;
        }

        [HttpGet("GetCart")]
        public async Task<ActionResult> GetCart(string id)
        {
            try
            {
                var cartDTO = await cartRepository.GetCart(id);

                if (cartDTO.Id != null)
                {
                    return Ok(cartDTO);
                }
                else
                    return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest($"Error al intentar obtener la categoria: {ex.Message}");
            }
        }

        [HttpPost("CreateCart")]
        public async Task<ActionResult> CreateCart(List<CarritoDTO> cartDTO)
        {
            try
            {
                var errors = validateCart(cartDTO);
                if (errors.Count() > 0)
                    return BadRequest(errors);

                List<Carrito> cart = mapper.Map<List<CarritoDTO>, List<Carrito>>(cartDTO);

                var sesion = await cartRepository.CreateSesion(cart[0], cartDTO[0].Email);
                foreach (var item in cart)
                {
                    item.SesionId = sesion.Id;
                }

                var result = await cartRepository.CreateCart(cart);

                if (result > 0)
                {
                    return Ok(cartDTO);
                }
                else
                    return BadRequest("No fue posible crear el carrito");
            }
            catch(Exception ex)
            {
                return BadRequest($"Error al intentar crear el carrito: {ex.Message}");
            }
        }

        [HttpPut("UpdateCart")]
        public async Task<ActionResult<List<CarritoDTO>>> UpdateCart(List<CarritoDTO> cartDTO)
        {
            try
            {
                var validation = validateUpdateCart(cartDTO);
                if (validation.Count() > 0)
                    return BadRequest(validation);

                var updateCart = mapper.Map<List<CarritoDTO>, List<Carrito>>(cartDTO);

                var sesion = await cartRepository.CreateSesion(updateCart[0], cartDTO[0].Email);
                foreach (var item in updateCart)
                {
                    item.SesionId = sesion.Id;
                }

                var cart = await cartRepository.UpdateCart(updateCart);

                if (cart.Count() > 0)
                {
                    var result = mapper.Map<List<Carrito>, List<CarritoDTO>>(cart);
                    return Ok(result);
                }
                else
                    return BadRequest("No fue posible actualizar el carrito");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el carrito: {ex.Message}");
            }
        }

        [HttpDelete("DeleteCart")]
        public async Task<ActionResult<string>> DeleteCart(string id)
        {
            try
            {
                var result = await cartRepository.DeleteCart(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar el carrito: {ex.Message}");
            }
        }

        private List<string> validateCart(List<CarritoDTO> cart)
        {
            List<string> errorList = new List<string>();

            foreach (var item in cart)
            {
                if (item.ProductoId == null)
                    errorList.Add("Es necesario especificar el Id de producto");

                if (item.Cantidad == 0)
                    errorList.Add("Es necesario la cantidad del producto productos");
            }

            return errorList;
        }

        private List<string> validateUpdateCart(List<CarritoDTO> cart)
        {
            List<string> errorList = new List<string>();

            foreach (var item in cart)
            {
                if (item.ProductoId == null)
                    errorList.Add("Es necesario especificar el Id de producto");

                if (item.Cantidad == 0)
                    errorList.Add("Es necesario la cantidad del producto productos");

                if (item.Id == null)
                    errorList.Add("Es necesario especificar el Id del carrito");
            }

            return errorList;
        }
    }
}
