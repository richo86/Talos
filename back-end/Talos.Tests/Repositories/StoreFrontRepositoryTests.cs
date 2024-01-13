using Domain.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Domain.DomainRepositories;
using FluentAssertions;
using Domain.Helper;
using Models.DTOs;

namespace Talos.Tests.Repositories
{
    [TestClass]
    public class StoreFrontRepositoryTests
    {
        public ServiceCollection Services { get; private set; }
        public ServiceProvider ServiceProvider { get; protected set; }
        private StoreFrontRepository storeFrontRepository;
        private ApplicationDbContext dbContext;
        private ProductHelper productHelper;

        public StoreFrontRepositoryTests()
        {
            Initialize();
            this.dbContext = ServiceProvider.GetService<ApplicationDbContext>();
            this.storeFrontRepository = new StoreFrontRepository(dbContext);
            this.productHelper = new ProductHelper(dbContext);
        }

        [TestInitialize]
        public void Initialize()
        {
            Services = new ServiceCollection();

            Services.AddDbContext<ApplicationDbContext>(opt => 
                opt.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()),
                ServiceLifetime.Scoped,
                ServiceLifetime.Scoped);

            ServiceProvider = Services.BuildServiceProvider();

            var dbContext = ServiceProvider.GetService<ApplicationDbContext>();

            dbContext.Areas.Add(new Models.Areas
            {
                Id = Guid.Parse("6F490FC4-7AE6-44E4-89EC-320FECDE07C7"),
                Descripcion = "Focus and energy",
                Codigo = "1",
                TipoCategoria = Models.Enums.TipoCategoria.Area,
                Imagen = null,
                ImagenBase64 = "base64"
            });

            dbContext.Categorias.Add(new Models.Categorias
            {
                Id = Guid.Parse("84742078-7313-45BB-9357-407AA5CB6791"),
                Descripcion = "Focus and energy",
                Area = Guid.Parse("6F490FC4-7AE6-44E4-89EC-320FECDE07C7"),
                Codigo = "1",
                TipoCategoria = Models.Enums.TipoCategoria.Principal,
                Imagen = null,
                ImagenBase64 = "base64"
            });

            dbContext.Subcategorias.Add(new Models.Subcategorias
            {
                Id = Guid.Parse("C678D5F3-BFC1-4168-8214-5D064F6811A2"),
                Descripcion = "Focus and energy",
                CategoriaPrincipal = Guid.Parse("84742078-7313-45BB-9357-407AA5CB6791"),
                Codigo = "1",
                TipoCategoria = Models.Enums.TipoCategoria.Secundario,
                Imagen = null,
                ImagenBase64 = "base64"
            });

            dbContext.Producto.Add(new Models.Classes.Producto
            {
                Id = Guid.Parse("832DF7D6-9341-45FE-B5C1-DAEAB46DBF3A"),
                Nombre = "Hocus Focus",
                Descripcion = "The perfect blend to give you that extra boost of energy",
                Codigo = null,
                Precio = (decimal)20.99,
                DescuentoId = Guid.Parse("332DF7D6-9341-45FE-B5C2-DAEAB46DBF3A"),
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now,
                CategoriaId = Guid.Parse("84742078-7313-45BB-9357-407AA5CB6791"),
                SubcategoriaId = Guid.Parse("C678D5F3-BFC1-4168-8214-5D064F6811A2"),
                Inventario = 100,
                Moneda = Guid.Empty
            });

            dbContext.Descuentos.Add(new Models.Classes.Descuentos
            {
                Id = Guid.Parse("332DF7D6-9341-45FE-B5C2-DAEAB46DBF3A"),
                Nombre = "Descuento de lanzamiento",
                Descripcion = "Descuento temporal por lanzamiento de la tienda",
                Estado = true,
                PorcentajeDescuento = 50,
                FechaCreacion = DateTime.Now,
                FechaEdicion = DateTime.Now
            });

            dbContext.Pais.Add(new Models.Classes.Pais
            {
                Id = Guid.Parse("932DF7D6-9341-45FE-B5C1-DAEAB46DBF3A"),
                Iso = "CO",
                Nombre = "COLOMBIA",
                NombreMin = "Colombia",
                Abreviacion = "COL",
                NumCode = "170",
                Codigo = "57"
            });

            dbContext.RegionesProductos.Add(new Models.Classes.RegionesProducto
            {
                Id = Guid.NewGuid(),
                Producto = Guid.Parse("832DF7D6-9341-45FE-B5C1-DAEAB46DBF3A"),
                Pais = Guid.Parse("932DF7D6-9341-45FE-B5C1-DAEAB46DBF3A"),
                Precio = (decimal)15.99,
                Moneda = Guid.NewGuid(),
                Inventario = 20,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            });

            dbContext.SaveChanges();
        }

        [Fact]
        public void GetAllMenuItems_true_returns_CategoriasProductoDTO()
        {
            //Arrange


            //Act
            var result = storeFrontRepository.GetAllMenuItems();

            //Assert
            result.Areas.Should().HaveCountGreaterThan(0);
            result.Areas.FirstOrDefault().Categorias.Should().HaveCountGreaterThan(0);

            Cleanup();
        }

        [Fact]
        public void GetBestSellers_returns_empty()
        {
            //Arrange


            //Act
            var result = storeFrontRepository.GetBestSellers("CO");

            //Assert
            result.Should().HaveCount(0);

            Cleanup();
        }

        [Theory]
        [InlineData("CO")]
        public void GetDiscountedProducts_returns_List<ProductoDTO>(string countryCode)
        {
            //Arrange
            var originalPrice = dbContext.Producto.FirstOrDefault().Precio;

            //Act
            var result = storeFrontRepository.GetDiscountedProducts(countryCode);

            //Assert
            result.Should().HaveCountGreaterThan(0);
            result.Should().BeOfType(typeof(List<Models.DTOs.ProductoDTO>));
            result.FirstOrDefault().Precio.Should().BeLessThan(originalPrice);
            result.FirstOrDefault().ValorDescuento.Should().NotBeNull();

            Cleanup();
        }

        [Theory]
        [InlineData("CO")]
        public void GetLatestsProducts_returns_List<ProductoDTO>(string countryCode)
        {
            //Arrange
            var originalPrice = dbContext.Producto.FirstOrDefault().Precio;

            //Act
            var result = storeFrontRepository.GetLatestProducts(countryCode);

            //Assert
            result.Should().HaveCountGreaterThan(0);
            result.Should().BeOfType(typeof(List<Models.DTOs.ProductoDTO>));
            result.FirstOrDefault().Precio.Should().BeLessThan(originalPrice);
            result.FirstOrDefault().ValorDescuento.Should().NotBeNull();

            Cleanup();
        }

        [Theory]
        [InlineData("CO")]
        public void GetLowestCost_returns_List<ProductoDTO>(string countryCode)
        {
            //Arrange
            var originalPrice = dbContext.Producto.FirstOrDefault().Precio;

            //Act
            var result = storeFrontRepository.GetLowestCost(countryCode);

            //Assert
            result.Should().HaveCountGreaterThan(0);
            result.Should().BeOfType(typeof(List<Models.DTOs.ProductoDTO>));
            result.FirstOrDefault().Precio.Should().BeLessThan(originalPrice);
            result.FirstOrDefault().ValorDescuento.Should().NotBeNull();

            Cleanup();
        }

        [Theory]
        [InlineData("CO", "6F490FC4-7AE6-44E4-89EC-320FECDE07C7")]
        public void GetProductsFromSpecificArea_returns_List<ProductoDTO>(string countryCode,string area)
        {
            //Arrange
            var originalPrice = dbContext.Producto.FirstOrDefault().Precio;

            //Act
            var result = storeFrontRepository.GetProductsFromSpecificArea(countryCode,area);

            //Assert
            result.Should().HaveCountGreaterThan(0);
            result.Should().BeOfType(typeof(List<Models.DTOs.ProductoDTO>));
            result.FirstOrDefault().Precio.Should().BeLessThan(originalPrice);
            result.FirstOrDefault().ValorDescuento.Should().NotBeNull();

            Cleanup();
        }

        [Theory]
        [InlineData("CO", "84742078-7313-45BB-9357-407AA5CB6791")]
        public void GetProductsFromSpecificCategory_returns_List<ProductoDTO>(string countryCode, string category)
        {
            //Arrange
            var originalPrice = dbContext.Producto.FirstOrDefault().Precio;

            //Act
            var result = storeFrontRepository.GetProductsFromSpecificCategory(countryCode, category);

            //Assert
            result.Should().HaveCountGreaterThan(0);
            result.Should().BeOfType(typeof(List<Models.DTOs.ProductoDTO>));
            result.FirstOrDefault().Precio.Should().BeLessThan(originalPrice);
            result.FirstOrDefault().ValorDescuento.Should().NotBeNull();

            Cleanup();
        }

        [Theory]
        [InlineData("CO", "C678D5F3-BFC1-4168-8214-5D064F6811A2")]
        public void GetProductsFromSpecificSubcategory_returns_List<ProductoDTO>(string countryCode, string subcategory)
        {
            //Arrange
            var originalPrice = dbContext.Producto.FirstOrDefault().Precio;

            //Act
            var result = storeFrontRepository.GetProductsFromSpecificSubcategory(countryCode, subcategory);

            //Assert
            result.Should().HaveCountGreaterThan(0);
            result.Should().BeOfType(typeof(List<Models.DTOs.ProductoDTO>));
            result.FirstOrDefault().Precio.Should().BeLessThan(originalPrice);
            result.FirstOrDefault().ValorDescuento.Should().NotBeNull();

            Cleanup();
        }

        [Theory]
        [InlineData("CO")]
        public void GetTopAreas_returns_List<CollectionDTO>(string countryCode)
        {
            //Arrange

            //Act
            var result = storeFrontRepository.GetTopAreas(countryCode);

            //Assert
            result.Should().HaveCountGreaterThan(0);
            result.Should().BeOfType(typeof(List<Models.DTOs.CollectionDTO>));

            Cleanup();
        }

        [Theory]
        [InlineData("CO")]
        public void GetTopSubcategories_returns_List<CollectionDTO>(string countryCode)
        {
            //Arrange

            //Act
            var result = storeFrontRepository.GetTopSubcategories(countryCode);

            //Assert
            result.Should().HaveCountGreaterThan(0);
            result.Should().BeOfType(typeof(List<Models.DTOs.CollectionDTO>));

            Cleanup();
        }

        [Theory]
        [InlineData("Focus", "CO")]
        [InlineData("best", "CO")]
        [InlineData("mejor", "CO")]
        [InlineData("all", "CO")]
        [InlineData("todos", "CO")]
        public async void SearchItems_returns_List<CollectionDTO>(string search, string countryCode)
        {
            //Arrange

            //Act
            var result = await storeFrontRepository.SearchItems(search,countryCode);

            //Assert
            result.Should().HaveCountGreaterThan(0);
            result.Should().BeOfType(typeof(List<SearchResults>));

            Cleanup();
        }

        [Theory]
        [InlineData("CO")]
        public void GetStoreProducts_returns_List<ProductoDTO>(string countryCode)
        {
            //Arrange
            var originalPrice = dbContext.Producto.FirstOrDefault().Precio;

            //Act
            var result = storeFrontRepository.GetAllProducts(countryCode);

            //Assert
            result.Should().HaveCountGreaterThan(0);
            result.Should().BeOfType(typeof(List<Models.DTOs.ProductoDTO>));
            result.FirstOrDefault().Precio.Should().BeLessThan(originalPrice);
            result.FirstOrDefault().ValorDescuento.Should().NotBeNull();

            Cleanup();
        }

        [Fact]
        public void GetStoreAreas_returns_list()
        {
            //Arrange

            //Act
            var result = storeFrontRepository.GetStoreAreas();

            //Assert
            result.Should().HaveCountGreaterThan(0);
            result.Should().BeOfType<List<Models.DTOs.CollectionDTO>>();

            Cleanup();
        }

        [Fact]
        public void GetStoreCategories_returns_list()
        {
            //Arrange

            //Act
            var result = storeFrontRepository.GetStoreCategories();

            //Assert
            result.Should().HaveCountGreaterThan(0);
            result.Should().BeOfType<List<Models.DTOs.CollectionDTO>>();

            Cleanup();
        }

        [Fact]
        public void GetStoreSubCategories_returns_list()
        {
            //Arrange

            //Act
            var result = storeFrontRepository.GetStoreSubcategories();

            //Assert
            result.Should().HaveCountGreaterThan(0);
            result.Should().BeOfType<List<Models.DTOs.CollectionDTO>>();

            Cleanup();
        }

        [TestCleanup]
        public virtual void Cleanup()
        {
            ServiceProvider.Dispose();
            ServiceProvider = null;
        }
    }
}
