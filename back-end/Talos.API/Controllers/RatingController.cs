using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Classes;
using Models.DTOs;
using System;
using System.Linq;
using System.Threading.Tasks;
using Talos.API.User;

namespace Talos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly IRatingRepository ratingRepository;

        public RatingController(UserManager<ApplicationUser> userManager, IMapper mapper, IRatingRepository ratingRepository)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.ratingRepository = ratingRepository;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post([FromBody] CalificacionDTO calificacionDTO)
        {
            try
            {
                var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email").Value;
                var usuario = await userManager.FindByEmailAsync(email);
                var calificacion = mapper.Map<CalificacionDTO, Calificacion>(calificacionDTO);
                calificacion.UsuarioId = usuario.Id;

                await ratingRepository.CreateOrUpdate(calificacion);

                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest($"Error al intentar realizar la puntuación: {ex.Message}");
            }
        }
    }
}
