using AutoMapper;
using Domain.Interfaces;
using Domain.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
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
    public class CategoryController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly ICategoryRepository categoryRepository;
        private readonly IDriveRepository driveRepository;

        public CategoryController(UserManager<ApplicationUser> userManager, IMapper mapper, ICategoryRepository categoryRepository, IDriveRepository driveRepository)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.categoryRepository = categoryRepository;
            this.driveRepository = driveRepository;
        }

        [HttpGet("GetArea")]
        public async Task<ActionResult> GetArea(string id)
        {
            try
            {
                var area = await categoryRepository.GetArea(id);

                if (area.Descripcion != null)
                {
                    var categoriaDTO = mapper.Map<Areas, CategoriaDTO>(area);

                    if(categoriaDTO.Imagen != null)
                        categoriaDTO.Imagen = driveRepository.GetFileById(categoriaDTO.Imagen);
                    
                    return Ok(categoriaDTO);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al intentar obtener la categoria: {ex.Message}");
            }
        }

        [HttpGet("GetCategory")]
        public async Task<ActionResult> GetCategory(string id)
        {
            try
            {
                var category = await categoryRepository.GetCategory(id);

                if (category.Descripcion != null)
                {
                    var categoriaDTO = mapper.Map<Categorias, CategoriaDTO>(category);

                    if (categoriaDTO.Imagen != null)
                        categoriaDTO.Imagen = driveRepository.GetFileById(categoriaDTO.Imagen);

                    return Ok(categoriaDTO);
                }
                else
                    return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest($"Error al intentar obtener la categoria: {ex.Message}");
            }
        }

        [HttpGet("GetSecondaryCategory")]
        public async Task<ActionResult> GetSecondaryCategory(string id)
        {
            try
            {
                var category = await categoryRepository.GetSecondaryCategory(id);

                if (category.Descripcion != null)
                {
                    var categoriaDTO = mapper.Map<Subcategorias, CategoriaDTO>(category);

                    if (categoriaDTO.Imagen != null)
                        categoriaDTO.Imagen = driveRepository.GetFileById(categoriaDTO.Imagen);

                    return Ok(categoriaDTO);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al intentar obtener la subcategoria: {ex.Message}");
            }
        }

        [HttpGet("GetMainCategories")]
        public async Task<ActionResult> GetMainCategories()
        {
            try
            {
                var categories = await categoryRepository.GetMainCategories();

                if (categories.Any())
                {
                    var categoriasDTO = mapper.Map<List<Categorias>, List<CategoriaDTO>>(categories);
                    return Ok(categoriasDTO);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al intentar obtener las categorias principales: {ex.Message}");
            }
        }

        [HttpGet("GetMainAreas")]
        public async Task<ActionResult> GetMainAreas()
        {
            try
            {
                var areas = await categoryRepository.GetMainAreas();

                if (areas.Any())
                {
                    var areasDTO = mapper.Map<List<Areas>, List<CategoriaDTO>>(areas);
                    return Ok(areasDTO);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al intentar obtener las áreas de negocio: {ex.Message}");
            }
        }

        [HttpGet("GetSecondaryCategories")]
        public async Task<ActionResult> GetSecondaryCategories()
        {
            try
            {
                var categories = await categoryRepository.GetSecondaryCategories();

                if (categories.Any())
                {
                    var categoriasDTO = mapper.Map<List<Subcategorias>, List<CategoriaDTO>>(categories);
                    return Ok(categoriasDTO);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al intentar obtener las categorias secundarias: {ex.Message}");
            }
        }

        [HttpGet("getAllCategories")]
        public async Task<ActionResult<CategoriaDTO>> GetAllCategories([FromQuery] PaginacionDTO paginacionDTO)
        {
            try
            {
                var queryable = categoryRepository.GetCategories();
                if (!queryable.Any())
                    return NotFound();

                await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
                var categories = await queryable.OrderBy(x => x.Descripcion).Paginar(paginacionDTO).ToListAsync();

                if (categories.Count() != 0)
                {
                    return Ok(categories);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al recuperar las categorias: {ex.Message}");
            }
        }

        [HttpGet("getAllSubcategories")]
        public async Task<ActionResult<CategoriaDTO>> GetAllSubcategories([FromQuery] PaginacionDTO paginacionDTO)
        {
            try
            {
                var queryable = categoryRepository.GetSubcategories();
                if (!queryable.Any())
                    return NotFound();

                await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
                var categories = await queryable.OrderBy(x => x.Descripcion).Paginar(paginacionDTO).ToListAsync();

                if (categories.Count() != 0)
                {
                    return Ok(categories);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al recuperar las subcategorias: {ex.Message}");
            }
        }

        [HttpGet("getAllAreas")]
        public async Task<ActionResult<CategoriaDTO>> GetAllAreas([FromQuery] PaginacionDTO paginacionDTO)
        {
            try
            {
                var queryable = categoryRepository.GetAreas();
                if (!queryable.Any())
                    return NotFound();

                await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
                var categories = await queryable.OrderBy(x => x.Descripcion).Paginar(paginacionDTO).ToListAsync();

                if (categories.Count() != 0)
                {
                    return Ok(categories);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al recuperar las areas: {ex.Message}");
            }
        }


        [HttpPost("CreateCategory")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult> CreateCategory(CategoriaDTO categoryDTO)
        {
            try
            {
                var errors = validateCategory(categoryDTO);
                if (errors.Count() > 0)
                    return BadRequest(errors);

                if(categoryDTO.TipoCategoria == Models.Enums.TipoCategoria.Principal)
                {
                    Categorias category = mapper.Map<CategoriaDTO, Categorias>(categoryDTO);

                    category = await categoryRepository.CreateCategory(category);

                    if (category.Descripcion != null)
                    {
                        var result = mapper.Map<Categorias, CategoriaDTO>(category);
                        return Ok(result);
                    }
                    else
                        return Ok("No fue posible crear la categoria o actualizarla");
                }
                else if (categoryDTO.TipoCategoria == Models.Enums.TipoCategoria.Area)
                {
                    Areas area = mapper.Map<CategoriaDTO, Areas>(categoryDTO);

                    area = await categoryRepository.CreateArea(area);

                    if (area.Descripcion != null)
                    {
                        var result = mapper.Map<Areas, CategoriaDTO>(area);
                        return Ok(result);
                    }
                    else
                        return Ok("No fue posible crear el área o actualizarla");
                }
                else
                {
                    Subcategorias subCategory = mapper.Map<CategoriaDTO, Subcategorias>(categoryDTO);

                    subCategory = await categoryRepository.CreateSubCategory(subCategory);

                    if (subCategory.Descripcion != null)
                    {
                        var result = mapper.Map<Subcategorias, CategoriaDTO>(subCategory);
                        return Ok(result);
                    }
                    else
                        return Ok("No fue posible crear la categoria o actualizarla");
                }
            }
            catch(Exception ex)
            {
                return BadRequest($"Error al intentar crear la categoria: {ex.Message}");
            }
        }

        [HttpPut("UpdateCategory")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult<CategoriaActDTO>> UpdateCategory(CategoriaActDTO categoryDTO)
        {
            try
            {
                var validation = validateCategoryAct(categoryDTO);
                if (validation.Count() > 0)
                    return BadRequest(validation);

                if (categoryDTO.TipoCategoria == Models.Enums.TipoCategoria.Principal)
                {
                    var updateCategory = mapper.Map<CategoriaActDTO, Categorias>(categoryDTO);

                    var category = await categoryRepository.UpdateCategory(updateCategory);

                    if (category.Descripcion != null)
                    {
                        var result = mapper.Map<Categorias, CategoriaDTO>(category);
                        return Ok(result);
                    }
                    else
                        return BadRequest("No fue posible actualizar el producto");
                }
                else if(categoryDTO.TipoCategoria == Models.Enums.TipoCategoria.Area)
                {
                    var updateArea = mapper.Map<CategoriaActDTO, Areas>(categoryDTO);

                    var area = await categoryRepository.UpdateArea(updateArea);

                    if (area.Descripcion != null)
                    {
                        var result = mapper.Map<Areas, CategoriaDTO>(area);
                        return Ok(result);
                    }
                    else
                        return BadRequest("No fue posible actualizar el producto");
                }
                else
                {
                    var updateCategory = mapper.Map<CategoriaActDTO, Subcategorias>(categoryDTO);

                    var category = await categoryRepository.UpdateSubcategory(updateCategory);

                    if (category.Descripcion != null)
                    {
                        var result = mapper.Map<Subcategorias, CategoriaDTO>(category);
                        return Ok(result);
                    }
                    else
                        return BadRequest("No fue posible actualizar el producto");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar la categoria: {ex.Message}");
            }
        }

        [HttpDelete("DeleteCategory")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult<string>> DeleteCategory(string id)
        {
            try
            {
                var result = await categoryRepository.DeleteCategory(id);

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest($"Error al eliminar la categoria: Ya existe una relación con un producto o categoria activo y no es posible eliminarlo. " +
                                    $"Por favor intenta editar el registro");
            }
        }

        private List<string> validateCategory(CategoriaDTO category)
        {
            List<string> errorList = new List<string>();

            if (category.Descripcion == null)
                errorList.Add("Es necesario incluir una descripción");

            return errorList;
        }

        private List<string> validateCategoryAct(CategoriaActDTO category)
        {
            List<string> errorList = new List<string>();

            if (category.Descripcion == null)
                errorList.Add("Es necesario incluir una descripción");

            return errorList;
        }
    }
}
