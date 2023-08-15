using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Talos.API.User;

namespace Talos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreFrontController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly IStoreFrontRepository storeFrontRepository;
        private readonly IDriveRepository driveRepository;

        public StoreFrontController(UserManager<ApplicationUser> userManager, IMapper mapper, IStoreFrontRepository storeFrontRepository, IDriveRepository driveRepository)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.storeFrontRepository = storeFrontRepository;
            this.driveRepository = driveRepository;
        }

        [HttpGet("GetAllProducts")]
        public async Task<ActionResult> GetAllMenuItems()
        {
            try
            {
                var products = await storeFrontRepository.GetAllMenuItems();

                if (!products.Areas.Any())
                    return NotFound();

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al intentar obtener las categorias: {ex.Message}");
            }
        }

        [HttpGet("GetLatestProducts")]
        public ActionResult GetLatestProducts(string countryCode)
        {
            try
            {
                var products = storeFrontRepository.GetLatestProducts(countryCode);

                if (!products.Any())
                    return NotFound();

                foreach (var item in products)
                {
                    item.ImagenesBase64 = driveRepository.GetFilesByIds(item.Imagenes);
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al intentar obtener los productos: {ex.Message}");
            }
        }

        [HttpGet("GetBestSellers")]
        public ActionResult GetBestSellers(string countryCode)
        {
            try
            {
                var products = storeFrontRepository.GetBestSellers(countryCode);

                if (!products.Any())
                    return NotFound();

                foreach (var item in products)
                {
                    item.ImagenesBase64 = driveRepository.GetFilesByIds(item.Imagenes);
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al intentar obtener los productos: {ex.Message}");
            }
        }

        [HttpGet("GetLowestCost")]
        public ActionResult GetLowestCost(string countryCode)
        {
            try
            {
                var products = storeFrontRepository.GetLowestCost(countryCode);

                if (!products.Any())
                    return NotFound();

                foreach (var item in products)
                {
                    item.ImagenesBase64 = driveRepository.GetFilesByIds(item.Imagenes);
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al intentar obtener los productos: {ex.Message}");
            }
        }

        [HttpGet("GetProductsFromCategory")]
        public ActionResult GetProductsFromCategory(string countryCode)
        {
            try
            {
                var products = storeFrontRepository.GetProductsFromCategory(countryCode);

                if (!products.Any())
                    return NotFound();

                foreach (var item in products)
                {
                    item.ImagenesBase64 = driveRepository.GetFilesByIds(item.Imagenes);
                }

                return Ok(products);
            }
            catch(Exception ex)
            {
                return BadRequest($"Error al intentar obtener los productos: {ex.Message}");
            }
        }

        [HttpGet("GetProductsFromSubcategory")]
        public ActionResult GetProductsFromSubcategory(string countryCode)
        {
            try
            {
                var products = storeFrontRepository.GetProductsFromSubcategory(countryCode);

                if (!products.Any())
                    return NotFound();

                foreach (var item in products)
                {
                    item.ImagenesBase64 = driveRepository.GetFilesByIds(item.Imagenes);
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al intentar obtener los productos: {ex.Message}");
            }
        }

        [HttpGet("GetProductsFromArea")]
        public ActionResult GetProductsFromArea(string countryCode)
        {
            try
            {
                var products = storeFrontRepository.GetProductsFromArea(countryCode);

                if (!products.Any())
                    return NotFound();

                foreach (var item in products)
                {
                    item.ImagenesBase64 = driveRepository.GetFilesByIds(item.Imagenes);
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al intentar obtener los productos: {ex.Message}");
            }
        }

        [HttpGet("GetProductsFromSpecificArea")]
        public ActionResult GetProductsFromSpecificArea(string countryCode, string area)
        {
            try
            {
                var products = storeFrontRepository.GetProductsFromSpecificArea(countryCode,area);

                if (!products.Any())
                    return NotFound();

                foreach (var item in products)
                {
                    item.ImagenesBase64 = driveRepository.GetFilesByIds(item.Imagenes);
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al intentar obtener los productos: {ex.Message}");
            }
        }

        [HttpGet("GetProductsFromSpecificCategory")]
        public ActionResult GetProductsFromSpecificCategory(string countryCode, string category)
        {
            try
            {
                var products = storeFrontRepository.GetProductsFromSpecificCategory(countryCode,category);

                if (!products.Any())
                    return NotFound();

                foreach (var item in products)
                {
                    item.ImagenesBase64 = driveRepository.GetFilesByIds(item.Imagenes);
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al intentar obtener los productos: {ex.Message}");
            }
        }

        [HttpGet("GetProductsFromSpecificSubcategory")]
        public ActionResult GetProductsFromSpecificSubcategory(string countryCode, string subcategory)
        {
            try
            {
                var products = storeFrontRepository.GetProductsFromSpecificSubcategory(countryCode,subcategory);

                if (!products.Any())
                    return NotFound();

                foreach (var item in products)
                {
                    item.ImagenesBase64 = driveRepository.GetFilesByIds(item.Imagenes);
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al intentar obtener los productos: {ex.Message}");
            }
        }

        [HttpGet("GetDiscountedProducts")]
        public ActionResult GetDiscountedProducts(string countryCode)
        {
            try
            {
                var products = storeFrontRepository.GetDiscountedProducts(countryCode);

                if (!products.Any())
                    return NotFound();

                foreach (var item in products)
                {
                    item.ImagenesBase64 = driveRepository.GetFilesByIds(item.Imagenes);
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al intentar obtener los productos: {ex.Message}");
            }
        }

        [HttpGet("GetTopAreas")]
        public ActionResult GetTopAreas(string countryCode)
        {
            try
            {
                var categories = storeFrontRepository.GetTopAreas(countryCode);

                if (!categories.Any())
                    return NotFound();

                foreach (var item in categories)
                {
                    item.Image = driveRepository.GetFileById(item.Image);
                }

                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al intentar obtener los productos: {ex.Message}");
            }
        }

        [HttpGet("GetTopSubcategories")]
        public ActionResult GetTopSubcategories(string countryCode)
        {
            try
            {
                var subcategories = storeFrontRepository.GetTopSubcategories(countryCode);

                if (!subcategories.Any())
                    return NotFound();

                foreach (var item in subcategories)
                {
                    item.Image = driveRepository.GetFileById(item.Image);
                }

                return Ok(subcategories);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al intentar obtener los productos: {ex.Message}");
            }
        }
    }
}
