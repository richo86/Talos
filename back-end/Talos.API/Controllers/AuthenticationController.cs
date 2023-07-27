using AutoMapper;
using Domain.Interfaces;
using Domain.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talos.API.User;

namespace Talos.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IAccountRepository accountRepository;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public AuthenticationController(IAccountRepository accountRepository, IConfiguration configuration, IMapper mapper)
        {
            this.accountRepository = accountRepository;
            this.configuration = configuration;
            this.mapper = mapper;
        }

        [HttpGet("users")]
        public async Task<ActionResult<List<UsuarioDTO>>> GetUsers([FromQuery] PaginacionDTO paginacionDTO)
        {
            try
            {
                var queryable = accountRepository.getUsers();
                if (queryable == null)
                    return NotFound();

                await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
                var usuarios = await queryable.OrderBy(x => x.Email).Paginar(paginacionDTO).ToListAsync();
                usuarios = await accountRepository.getRoles(usuarios);
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("usuario")]
        public async Task<ActionResult<UsuarioDTO>> GetUser(string id)
        {
            try
            {
                var usuario = await accountRepository.getUser(id);
                return mapper.Map<UsuarioDTO>(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("getUserID")]
        public async Task<ActionResult<string>> GetUserID(string email)
        {
            try
            {
                var id = await accountRepository.getUserID(email);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost("makeAdmin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult> MakeAdmin(string id)
        {
            try
            {
                var result = await accountRepository.MakeAdmin(id);

                if (result)
                    return NoContent();

                return BadRequest("No fue posible realizar la operación");
            }
            catch(Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost("deleteAdmin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult> DeleteAdmin(string id)
        {
            try
            {
                var result = await accountRepository.DeleteAdmin(id);

                if (result)
                    return NoContent();

                return BadRequest("No fue posible realizar la operación");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<RespuestaAutenticacion>> Create([FromBody] UsuarioDTO usuario)
        {
            try
            {
                var user = mapper.Map<UsuarioDTO, ApplicationUser>(usuario);

                var resultado = await accountRepository.Create(user, usuario.Password);

                if (resultado)
                {
                    var credenciales = mapper.Map<Credenciales>(usuario);
                    return await CreateToken(credenciales,user.FirstName);
                }
                else
                    return BadRequest("No fue posible crear el usuario");
            }
            catch(Exception ex)
            {
                return BadRequest($"Error al crear el usuario. Error: {ex.Message}");
            }
        }

        [HttpPost("signin")]
        public async Task<ActionResult<RespuestaAutenticacion>> SignIn([FromBody] Credenciales credenciales)
        {
            try
            {
                var user = await accountRepository.SignIn(credenciales);
                if (!string.IsNullOrEmpty(user.FirstName))
                    return await CreateToken(credenciales,user.FirstName);
                else
                    return BadRequest("No fue posible iniciar sesión, por favor verifica tu usuario y contraseña");
            }
            catch(Exception ex)
            {
                return BadRequest($"No fue posible iniciar sesión. Error: {ex.Message}");
            }
        }

        [HttpDelete("deleteUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var respuesta = await accountRepository.Delete(id);

                    if (!respuesta)
                        return BadRequest("No fue posible eliminar el usuario");
                }

                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest($"Error al eliminar el usuario: { ex.Message }");
            }
        }

        [HttpPut("updateUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> UpdateUser(UsuarioDTO usuario)
        {
            try
            {
                var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email").Value;
                var role = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "role").Value;
                if (email == usuario.Email || role == "admin")
                {
                    var respuesta = await accountRepository.Update(usuario);

                    if (respuesta.UserName != null)
                    {
                        return Ok(mapper.Map<UsuarioDTO>(respuesta));
                    }
                    else
                        return BadRequest("No fue posible editar el usuario");
                }
                return BadRequest("Ha ocurrido un error al editar el perfil");
            }
            catch(Exception ex)
            {
                return BadRequest($"Error al editar el usuario: { ex.Message }");
            }
        }

        [HttpGet("generos")]
        public ActionResult<List<GeneroDTO>> GetGenders()
        {
            try
            {
                var generos = accountRepository.getGenders();
                
                return mapper.Map<List<GeneroDTO>>(generos);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("paises")]
        public ActionResult<List<PaisDTO>> GetCountries()
        {
            try
            {
                var paises = accountRepository.getCountries();

                return mapper.Map<List<PaisDTO>>(paises);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        private async Task<RespuestaAutenticacion> CreateToken(Credenciales credenciales, string nombre)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", credenciales.Email),
                new Claim("nombre", nombre)
            };

            var usuario = await accountRepository.GetUserByEmail(credenciales.Email);
            var claimsDB = await accountRepository.GetClaims(usuario);

            claims.AddRange(claimsDB);

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llaveJwt"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddYears(1);
            var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims
                , expires: expiracion, signingCredentials: creds);

            return new RespuestaAutenticacion { Token = new JwtSecurityTokenHandler().WriteToken(token), Expiracion = expiracion };
        }
    }
}
