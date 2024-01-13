using AutoMapper;
using Domain.Interfaces;
using Domain.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository paymentRepository;

        public PaymentController(UserManager<ApplicationUser> userManager, IMapper mapper, IPaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }

        [HttpGet("GetPayment")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> GetPayment(string id)
        {
            try
            {
                var detallePedidosDTO = await paymentRepository.GetPayment(id);

                if (detallePedidosDTO.NombreUsuario != null)
                {
                    return Ok(detallePedidosDTO);
                }
                else
                    return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest($"Error al intentar obtener la información de pago: {ex.Message}");
            }
        }

        [HttpGet("GetPayments")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> GetPayments([FromQuery] PaginacionDTO paginacionDTO)
        {
            try
            {
                var queryable = await paymentRepository.GetPayments();
                if (queryable == null)
                    return NotFound();

                await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
                var detallePedidosDTO = queryable.OrderBy(x => x.FechaActualizacion).Paginar(paginacionDTO).ToList();

                if (detallePedidosDTO.Count() > 0)
                {
                    return Ok(detallePedidosDTO);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al intentar obtener la información de pago: {ex.Message}");
            }
        }

        [HttpGet("GetPaymentsTypes")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> GetPaymentsTypes()
        {
            try
            {
                var paymentTypes = await paymentRepository.GetPaymentTypes();

                if (paymentTypes.Count() > 0)
                {
                    return Ok(paymentTypes);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al intentar obtener la información de pago: {ex.Message}");
            }
        }

        [HttpPost("CreatePayment")]
        public async Task<ActionResult> CreatePayment(PaymentDTO paymentDTO)
        {
            try
            {
                var errors = validatePayment(paymentDTO);
                if (errors.Count() > 0)
                    return BadRequest(errors);

                var paymentResult = await paymentRepository.CreatePayment(paymentDTO);

                if (paymentResult == "¡Operación exitosa!")
                    return Ok(paymentResult);
                else
                    return BadRequest(paymentResult);
            }
            catch(Exception ex)
            {
                return BadRequest($"Error al intentar ejecutar el pago: {ex.Message}");
            }
        }

        [HttpPut("UpdatePayment")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult<PaymentDTO>> UpdatePayment(PaymentDTO paymentDTO)
        {
            try
            {
                var validation = validatePayment(paymentDTO);
                if (validation.Count() > 0)
                    return BadRequest(validation);

                var update = await paymentRepository.UpdatePayment(paymentDTO);

                if (update.MetodoPago != null)
                {
                    return Ok(update);
                }
                else
                    return BadRequest("No fue posible actualizar el pago");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el pago: {ex.Message}");
            }
        }

        private List<string> validatePayment(PaymentDTO payment)
        {
            List<string> errorList = new List<string>();

            if (payment.MetodoPago == null)
                errorList.Add("Es necesario incluir el metodo de pago");

            if (payment.TipoVenta == null)
                errorList.Add("Es necesario incluir el tipo de venta");

            if (payment.Usuario.Address == null || payment.Usuario.Country == null
                    || payment.Usuario.Email == null || payment.Usuario.FirstName == null
                    || payment.Usuario.PhoneNumber == null || payment.Usuario.UserIP == null)
                errorList.Add("Es necesario completar toda la información del usuario");

            return errorList;
        }
    }
}
