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
using Talos.API.Classes;
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
        private readonly CategoriesHelper categoryHelper;

        public CategoryController(UserManager<ApplicationUser> userManager, IMapper mapper, ICategoryRepository categoryRepository, IDriveRepository driveRepository)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.categoryRepository = categoryRepository;
            this.driveRepository = driveRepository;
            this.categoryHelper = new CategoriesHelper(this.driveRepository);
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
                    categoriaDTO = categoryHelper.GetImage(categoriaDTO);
                    
                    return Ok(categoriaDTO);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"There was an error obtaining the area: {ex.Message}");
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
                    categoriaDTO = categoryHelper.GetImage(categoriaDTO);

                    return Ok(categoriaDTO);
                }
                else
                    return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest($"There was an error obtaining the area: {ex.Message}");
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
                    categoriaDTO = categoryHelper.GetImage(categoriaDTO);

                    return Ok(categoriaDTO);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"There was an error obtaining the subcategory: {ex.Message}");
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
                return BadRequest($"An error occurred when trying to get the main categories: {ex.Message}");
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
                return BadRequest($"An error occurred when trying to get the areas: {ex.Message}");
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
                return BadRequest($"An error occurred when trying to get the subcategories: {ex.Message}");
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
                return BadRequest($"An error occurred when trying to obtain the categories: {ex.Message}");
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
                return BadRequest($"An error occurred when trying to obtain the subcategories: {ex.Message}");
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
                return BadRequest($"An error occurred when trying to obtain the areas: {ex.Message}");
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
                        return Ok("An error occurred while trying to create the category");
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
                        return Ok("An error occurred while trying to create the area");
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
                        return Ok("An error occurred while trying to create the subcategory");
                }
            }
            catch(Exception ex)
            {
                return BadRequest($"An error occurred while trying to create the category: {ex.Message}");
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
                        return BadRequest("An error occurred while trying to update the category");
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
                        return BadRequest("An error occurred while trying to update the area");
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
                        return BadRequest("An error occurred while trying to update the subcategory");
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
                return BadRequest($"An error occurred while trying to delete the category: A relationship between a product " +
                    $"or category has already been established and it is not possible to eliminate, please edit the category instead.");
            }
        }

        [HttpGet("GetCategoriesFromArea")]
        public async Task<ActionResult<List<CategoriaDTO>>> GetCategoriesFromArea(string id)
        {
            try
            {
                var categories = await categoryRepository.GetCategoriesFromArea(id);
                if (!categories.Any())
                    return NotFound();

                categories = categoryHelper.GetImageFromList(categories);

                return categories;
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred when trying to obtain the areas: {ex.Message}");
            }
        }

        [HttpGet("GetSubcategoriesFromCategory")]
        public async Task<ActionResult<List<CategoriaDTO>>> GetSubcategoriesFromCategory(string id)
        {
            try
            {
                var categories = await categoryRepository.GetSubcategoriesFromCategory(id);
                if (!categories.Any())
                    return NotFound();

                categories = categoryHelper.GetImageFromList(categories);

                return categories;
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred when trying to obtain the areas: {ex.Message}");
            }
        }

        private List<string> validateCategory(CategoriaDTO category)
        {
            List<string> errorList = new List<string>();

            if (category.Descripcion == null)
                errorList.Add("It is necessary to include a description");

            return errorList;
        }

        private List<string> validateCategoryAct(CategoriaActDTO category)
        {
            List<string> errorList = new List<string>();

            if (category.Descripcion == null)
                errorList.Add("It is necessary to include a description");

            return errorList;
        }
    }
}
