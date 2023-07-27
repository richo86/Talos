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
    public class DiscountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly IDiscountRepository discountRepository;

        public DiscountController(UserManager<ApplicationUser> userManager, IMapper mapper, IDiscountRepository discountRepository)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.discountRepository = discountRepository;
        }

        [HttpGet("GetDiscount")]
        public async Task<ActionResult> GetDiscount(string id)
        {
            try
            {
                var discount = await discountRepository.GetDiscount(id);

                if (discount.Descripcion != null)
                {
                    return Ok(discount);
                }
                else
                    return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest($"Error al intentar obtener el descuento: {ex.Message}");
            }
        }

        [HttpGet("getAllDiscounts")]
        public async Task<ActionResult<Descuentos>> GetAllDiscounts([FromQuery] PaginacionDTO paginacionDTO)
        {
            try
            {
                var queryable = discountRepository.GetDiscounts();
                if (!queryable.Any())
                    return NotFound();

                await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
                var discounts = await queryable.OrderBy(x => x.FechaCreacion).Paginar(paginacionDTO).ToListAsync();

                if (discounts.Count() != 0)
                {
                    var discountsDTO = mapper.Map<List<Descuentos>, List<DescuentoDTO>>(discounts);
                    return Ok(discountsDTO);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al recuperar los descuentos: {ex.Message}");
            }
        }

        [HttpGet("getAllDiscountsList")]
        public async Task<ActionResult<Descuentos>> getAllDiscountsList()
        {
            try
            {
                var discounts = await discountRepository.GetAllDiscounts();

                if (discounts.Count() != 0)
                {
                    return Ok(discounts);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al recuperar los descuentos: {ex.Message}");
            }
        }

        [HttpPost("CreateDiscount")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult> CreateDiscount(DescuentoDTO discountDTO)
        {
            try
            {
                var errors = validateDiscount(discountDTO);
                if (errors.Count() > 0)
                    return BadRequest(errors);

                Descuentos discount = mapper.Map<DescuentoDTO, Descuentos>(discountDTO);

                discount = await discountRepository.CreateDiscount(discount);

                if (discount.Descripcion != null)
                {
                    return Ok("Descuento creado de manera correcta");
                }
                else
                    return BadRequest("No fue posible crear el descuento");
            }
            catch(Exception ex)
            {
                return BadRequest($"Error al intentar crear el descuento: {ex.Message}");
            }
        }

        [HttpPut("UpdateDiscount")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult<string>> UpdateDiscount(DescuentoDTO discountDTO)
        {
            try
            {
                var validation = validateDiscount(discountDTO);
                if (validation.Count() > 0)
                    return BadRequest(validation);

                var discount = mapper.Map<DescuentoDTO, Descuentos>(discountDTO);

                discount = await discountRepository.UpdateDiscount(discount);

                if (discount.Descripcion != null)
                {
                    return Ok("Se ha actualizado el descuento exitosamente");
                }
                else
                    return BadRequest("No fue posible actualizar el descuento");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el descuento: {ex.Message}");
            }
        }

        [HttpDelete("DeleteDiscount")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult<string>> DeleteDiscount(string id)
        {
            try
            {
                var result = await discountRepository.DeleteDiscount(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar el descuento: {ex.Message}");
            }
        }

        private List<string> validateDiscount(DescuentoDTO discount)
        {
            List<string> errorList = new List<string>();

            if (discount.Descripcion == null)
                errorList.Add("Es necesario especificar una descripción");

            if (discount.Nombre == null)
                errorList.Add("Es necesario incluir un nombre");

            return errorList;
        }
    }
}
