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
using Models.Classes;

namespace Talos.Tests.Repositories
{
    [TestClass]
    public class ProductRepositoryTests
    {
        public ServiceCollection Services { get; private set; }
        public ServiceProvider ServiceProvider { get; protected set; }
        private ProductRepository _productRepository;
        private ApplicationDbContext _dbContext;

        public ProductRepositoryTests()
        {
            Initialize();
            _dbContext = ServiceProvider.GetService<ApplicationDbContext>();
            _productRepository = new ProductRepository(_dbContext);
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
                Codigo = "1",
                TipoCategoria = Models.Enums.TipoCategoria.Principal,
                Imagen = null,
                ImagenBase64 = "base64"
            });

            dbContext.Subcategorias.Add(new Models.Subcategorias
            {
                Id = Guid.Parse("C678D5F3-BFC1-4168-8214-5D064F6811A2"),
                Descripcion = "Focus and energy",
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
                DescuentoId = null,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now,
                CategoriaId = Guid.Parse("84742078-7313-45BB-9357-407AA5CB6791"),
                SubcategoriaId = Guid.Parse("C678D5F3-BFC1-4168-8214-5D064F6811A2"),
                Inventario = 100,
                Moneda = Guid.Empty
            });

            dbContext.Imagenes.Add(new Imagenes()
            {
                Id = Guid.Parse("832DF7D6-9342-45FE-B5C1-DAEAB46DBF1A"),
                ProductoId = Guid.Parse("832DF7D6-9341-45FE-B5C1-DAEAB46DBF3A"),
                ImagenUrl = "ImagenUrl",
                ImagenBase64 = "ImagenBase64"
            });

            dbContext.SaveChanges();
        }

        [Fact]
        public void AssignKeywords_returns_true()
        {
            //Arrange
            var productId = Guid.Parse("832DF7D6-9341-45FE-B5C1-DAEAB46DBF3A");
            var currentKeywordsCount = _dbContext.ProductKeywords
                                        .Where(x=>x.ProductId
                                        .Equals(productId))
                                        .Count();
            var keywords = new List<string>();
            keywords.Add("keyword");

            //Act
            var result = _productRepository.AssignKeywords(keywords, productId);

            //Assert
            result.Should().BeTrue();
            _dbContext.ProductKeywords.Where(x=>x.ProductId.Equals(productId)).Count()
                .Should().BeGreaterThan(currentKeywordsCount);

            Cleanup();
        }

        [Fact]
        public void Create_Image_Returns_true()
        {
            //Arrange
            var id = Guid.NewGuid().ToString();
            var product = _dbContext.Producto.FirstOrDefault().Id.ToString();
            var imageBase64 = "newBase64";
            var expectedCount = _dbContext.Imagenes.Count() + 1;

            //Act
            var result = _productRepository.CreateImage(id,product,imageBase64);

            //Assert
            result.Should().BeTrue();
            _dbContext.Imagenes.Count().Should().Be(expectedCount);
            Cleanup();
        }

        [Fact]
        public async void Create_Product_returns_object()
        {
            //Arrange
            var product = new Producto()
            {
                Id = Guid.NewGuid(),
                Nombre = "Test Product",
                Descripcion = "The perfect blend to give you that extra boost of energy",
                Codigo = null,
                Precio = (decimal)20.99,
                DescuentoId = null,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now,
                CategoriaId = Guid.Parse("84742078-7313-45BB-9357-407AA5CB6791"),
                SubcategoriaId = Guid.Parse("C678D5F3-BFC1-4168-8214-5D064F6811A2"),
                Inventario = 100,
                Moneda = Guid.Empty
            };

            //Act
            var result = await _productRepository.CreateProduct(product);

            //Assert
            result.Should().BeEquivalentTo(product);
            Cleanup();
        }

        [Fact]
        public async void Delete_Product_Return_string()
        {
            //Arrange
            var product = new Producto()
            {
                Id = Guid.NewGuid(),
                Nombre = "Test Product",
                Descripcion = "The perfect blend to give you that extra boost of energy",
                Codigo = null,
                Precio = (decimal)20.99,
                DescuentoId = null,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now,
                CategoriaId = Guid.Parse("84742078-7313-45BB-9357-407AA5CB6791"),
                SubcategoriaId = Guid.Parse("C678D5F3-BFC1-4168-8214-5D064F6811A2"),
                Inventario = 100,
                Moneda = Guid.Empty
            };
            var expectedCount = _dbContext.Producto.Count();

            //Act
            var createProduct = await _productRepository.CreateProduct(product);
            var deleteProduct = await _productRepository.DeleteProduct(product.Id.ToString());

            //Assert
            createProduct.Should().BeEquivalentTo(product);
            deleteProduct.Should().Be("Producto eliminado correctamente");
            _dbContext.Producto.Count().Should().Be(expectedCount);
            Cleanup();
        }

        [Fact]
        public async void GetProduct_returns_object()
        {
            //Arrange
            var id = _dbContext.Producto.FirstOrDefault().Id.ToString();
            var product = _dbContext.Producto
                        .Include(x => x.Categoria)
                        .Include(x => x.Subcategoria)
                        .Include(x => x.Imagenes).FirstOrDefault();

            //Act
            var result = await _productRepository.GetProduct(id);

            //Assert
            result.Id.Should().Be(product.Id);
            Cleanup();
        }

        [Fact]
        public void getProductBase64Images_returns_list()
        {
            //Arrange
            var id = _dbContext.Producto.FirstOrDefault().Id.ToString();
            var expectedCount = _dbContext.Imagenes.Count();

            //Act
            var result = _productRepository.getProductBase64Images(id);

            //Assert
            result.Should().HaveCount(expectedCount);
            Cleanup();
        }

        [Fact]
        public void getProductIds_returns_list()
        {
            //Arrange
            var id = _dbContext.Producto.FirstOrDefault().Id.ToString();
            var expectedCount = _dbContext.Imagenes.Count();

            //Act
            var result = _productRepository.getProductIds(id);

            //Assert
            result.Should().HaveCount(expectedCount);
            Cleanup();
        }

        [Fact]
        public void GetProducts_returns_IQueryable()
        {
            //Arrange
            var products = _dbContext.Producto.AsNoTracking()
                            .Include(x => x.Categoria)
                            .Include(x => x.Subcategoria)
                            .Include(x => x.Imagenes.Take(1))
                            .Where(x => x.Descripcion != null);

            //Act
            var result = _productRepository.GetProducts();

            //Assert
            result.Should().HaveCount(products.Count());
            result.FirstOrDefault().Id.Should().Be(products.FirstOrDefault().Id);
            Cleanup();
        }

        [Fact]
        public async void UpdateProduct_returns_object()
        {
            //Arrange
            var product = new Producto()
            {
                Id = _dbContext.Producto.FirstOrDefault().Id,
                Nombre = "Test Product edition",
                Descripcion = "The perfect blend to give you that extra boost of energy",
                Codigo = null,
                Precio = (decimal)20.99,
                DescuentoId = null,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now,
                CategoriaId = Guid.Parse("84742078-7313-45BB-9357-407AA5CB6791"),
                SubcategoriaId = Guid.Parse("C678D5F3-BFC1-4168-8214-5D064F6811A2"),
                Inventario = 100,
                Moneda = Guid.Empty
            };

            //Act
            var result = await _productRepository.UpdateProduct(product);

            //Assert
            result.Should().BeEquivalentTo(product);
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
