using AutoMapper;
using Domain.DomainRepositories;
using Domain.Helper;
using Domain.Interfaces;
using Domain.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Classes;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talos.API.Classes;

namespace Talos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICampaignRepository campaignRepository;
        private readonly IDriveRepository driveRepository;
        private readonly ProductsHelper productHelper;

        public CampaignController(IMapper mapper, ICampaignRepository campaignRepository, IDriveRepository driveRepository)
        {
            this.mapper = mapper;
            this.campaignRepository = campaignRepository;
            this.driveRepository = driveRepository;
            this.productHelper = new ProductsHelper(this.driveRepository);
        }

        [HttpGet("GetCampaign")]
        public async Task<ActionResult> GetCampaign(string id)
        {
            try
            {
                var campaign = await campaignRepository.GetCampaign(id);

                if (campaign.Descripcion != null)
                {
                    return Ok(campaign);
                }
                else
                    return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest($"Error al intentar obtener la campaña: {ex.Message}");
            }
        }

        [HttpGet("getAllCampaigns")]
        public async Task<ActionResult<Descuentos>> getAllCampaigns([FromQuery] PaginacionDTO paginacionDTO)
        {
            try
            {
                var queryable = campaignRepository.GetCampaigns();
                if (!queryable.Any())
                    return NotFound();

                await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
                var campaigns = queryable.OrderBy(x => x.FechaCreacion).Paginar(paginacionDTO).ToList();

                if (campaigns.Count() != 0)
                {
                    var campaignsDTO = mapper.Map<List<Campañas>, List<CampaignDTO>>(campaigns);
                    return Ok(campaignsDTO);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al recuperar las campañas: {ex.Message}");
            }
        }

        [HttpGet("getAllCampaignsList")]
        public ActionResult<Descuentos> getAllCampaignsList()
        {
            try
            {
                var discounts = campaignRepository.GetAllCampaigns();

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

        [HttpPost("CreateCampaign")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult> CreateCampaign(CampaignDTO campaignDTO)
        {
            try
            {
                var errors = validateCampaign(campaignDTO);
                if (errors.Count() > 0)
                    return BadRequest(errors);

                Campañas campaign = mapper.Map<CampaignDTO, Campañas>(campaignDTO);

                campaign = await campaignRepository.CreateCampaign(campaign);

                if (campaign.Descripcion != null)
                {
                    campaignRepository.InsertCampaignProducts(campaignDTO.Productos,campaign.Id);

                    return Ok(campaign.Id);
                }
                else
                    return BadRequest("No fue posible crear la campaña");
            }
            catch(Exception ex)
            {
                return BadRequest($"Error al intentar crear la campaña: {ex.Message}");
            }
        }

        [HttpPut("UpdateCampaign")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult<string>> UpdateCampaign(CampaignDTO campaignDTO)
        {
            try
            {
                var validation = validateCampaign(campaignDTO);
                if (validation.Count() > 0)
                    return BadRequest(validation);

                var campaign = mapper.Map<CampaignDTO, Campañas>(campaignDTO);

                campaign = await campaignRepository.UpdateCampaign(campaign);

                if (campaign.Descripcion != null)
                {
                    campaignRepository.InsertCampaignProducts(campaignDTO.Productos, campaign.Id);

                    return Ok("Se ha actualizado la campaña exitosamente");
                }
                else
                    return BadRequest("No fue posible actualizar la campaña");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar la campaña: {ex.Message}");
            }
        }

        [HttpDelete("DeleteCampaign")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult<string>> DeleteCampaign(string id)
        {
            try
            {
                var result = await campaignRepository.DeleteCampaign(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar la campaña: {ex.Message}");
            }
        }

        [HttpGet("getCampaignBase64Images")]
        public ActionResult<List<KeyValuePair<string, string>>> getCampaignBase64Images(string id)
        {
            try
            {
                var images = campaignRepository.getCampaignBase64Images(id);
                images = productHelper.VerifyProductImages(images);

                if (images.Any())
                    return Ok(images);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al recuperar el listado de imagenes base64: {ex.Message}");
            }
        }

        [HttpGet("GetAllProducts")]
        public ActionResult<List<ProductoDTO>> GetAllProducts()
        {
            try
            {
                var products = campaignRepository.GetAllProducts();

                if (products.Any())
                    return Ok(products);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al recuperar el listado de productos: {ex.Message}");
            }
        }

        private List<string> validateCampaign(CampaignDTO campaign)
        {
            List<string> errorList = new List<string>();

            if (campaign.Descripcion == null)
                errorList.Add("Es necesario especificar una descripción");

            if (campaign.Nombre == null)
                errorList.Add("Es necesario incluir un nombre");

            return errorList;
        }
    }
}
