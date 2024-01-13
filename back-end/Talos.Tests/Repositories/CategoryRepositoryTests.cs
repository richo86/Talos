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
using Models;
using Models.DTOs;

namespace Talos.Tests.Repositories
{
    [TestClass]
    public class CategoryRepositoryTests
    {
        public ServiceCollection Services { get; private set; }
        public ServiceProvider ServiceProvider { get; protected set; }
        private CategoryRepository categoryRepository;
        private ApplicationDbContext dbContext;

        public CategoryRepositoryTests()
        {
            Initialize();
            this.dbContext = ServiceProvider.GetService<ApplicationDbContext>();
            this.categoryRepository = new CategoryRepository(dbContext);
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
                Area = Guid.Parse("6F490FC4-7AE6-44E4-89EC-320FECDE07C7"),
                Imagen = null,
                ImagenBase64 = "base64"
            });

            dbContext.Subcategorias.Add(new Models.Subcategorias
            {
                Id = Guid.Parse("C678D5F3-BFC1-4168-8214-5D064F6811A2"),
                Descripcion = "Focus and energy",
                Codigo = "1",
                CategoriaPrincipal = Guid.Parse("84742078-7313-45BB-9357-407AA5CB6791"),
                TipoCategoria = Models.Enums.TipoCategoria.Secundario,
                Imagen = null,
                ImagenBase64 = "base64"
            });

            dbContext.SaveChanges();
        }

        [Fact]
        public async void Create_Category_Returns_object()
        {
            //Arrange
            var categoria = new Categorias()
            {
                Id = Guid.NewGuid(),
                Descripcion = "Test category",
                Area = dbContext.Areas.FirstOrDefault().Id,
                Imagen = null,
                ImagenBase64 = null,
                TipoCategoria = Models.Enums.TipoCategoria.Principal
            };
            var currentRecords = dbContext.Categorias.Count();
            var expectedRecords = currentRecords + 1;

            //Act
            var result = await categoryRepository.CreateCategory(categoria);

            //Assert
            result.Should().BeSameAs(categoria);
            dbContext.Categorias.Count().Should().Be(expectedRecords);
            Cleanup();
        }

        [Fact]
        public async void Create_Subcategory_Returns_object()
        {
            //Arrange
            var subcategoria = new Subcategorias()
            {
                Id = Guid.NewGuid(),
                Descripcion = "Test subcategory",
                CategoriaPrincipal = dbContext.Categorias.FirstOrDefault().Id,
                Imagen = null,
                ImagenBase64 = null,
                TipoCategoria = Models.Enums.TipoCategoria.Secundario
            };
            var currentRecords = dbContext.Categorias.Count();
            var expectedRecords = currentRecords + 1;

            //Act
            var result = await categoryRepository.CreateSubCategory(subcategoria);

            //Assert
            result.Should().BeSameAs(subcategoria);
            dbContext.Subcategorias.Count().Should().Be(expectedRecords);
            Cleanup();
        }

        [Fact]
        public async void create_Area_Returns_object()
        {
            //Arrange
            var area = new Areas()
            {
                Id = Guid.NewGuid(),
                Descripcion = "Test area",
                Imagen = null,
                ImagenBase64 = null,
                TipoCategoria = Models.Enums.TipoCategoria.Area
            };
            var currentRecords = dbContext.Categorias.Count();
            var expectedRecords = currentRecords + 1;

            //Act
            var result = await categoryRepository.CreateArea(area);

            //Assert
            result.Should().BeSameAs(area);
            dbContext.Areas.Count().Should().Be(expectedRecords);
            Cleanup();
        }

        [Fact]
        public async void Delete_Category_Returns_string()
        {
            //Arrange
            var area = new Areas()
            {
                Id = Guid.NewGuid(),
                Descripcion = "Test category",
                Imagen = null,
                ImagenBase64 = null,
                TipoCategoria = Models.Enums.TipoCategoria.Area
            };
            var categoria = new Categorias()
            {
                Id = Guid.NewGuid(),
                Descripcion = "Test category",
                Area = dbContext.Areas.FirstOrDefault().Id,
                Imagen = null,
                ImagenBase64 = null,
                TipoCategoria = Models.Enums.TipoCategoria.Principal
            };
            var subcategoria = new Subcategorias()
            {
                Id = Guid.NewGuid(),
                Descripcion = "Test subcategory",
                CategoriaPrincipal = dbContext.Categorias.FirstOrDefault().Id,
                Imagen = null,
                ImagenBase64 = null,
                TipoCategoria = Models.Enums.TipoCategoria.Secundario
            };
            var currentRecordsArea = dbContext.Areas.Count();
            var currentRecordsCategory = dbContext.Categorias.Count();
            var currentRecordsSubcategory = dbContext.Subcategorias.Count();

            //Act
            var createArea = await categoryRepository.CreateArea(area);
            var deleteArea = await categoryRepository.DeleteCategory(area.Id.ToString());
            var createCategory = await categoryRepository.CreateCategory(categoria);
            var deleteCategory = await categoryRepository.DeleteCategory(categoria.Id.ToString());
            var createSubcategory = await categoryRepository.CreateSubCategory(subcategoria);
            var deleteSubcategory = await categoryRepository.DeleteCategory(subcategoria.Id.ToString());

            //Assert
            createArea.Should().BeSameAs(area);
            dbContext.Areas.Count().Should().Be(currentRecordsArea);
            deleteArea.Should().Be(area.Id.ToString());
            createCategory.Should().BeSameAs(categoria);
            dbContext.Categorias.Count().Should().Be(currentRecordsCategory);
            deleteCategory.Should().Be(categoria.Id.ToString());
            createSubcategory.Should().BeSameAs(subcategoria);
            dbContext.Subcategorias.Count().Should().Be(currentRecordsSubcategory);
            deleteSubcategory.Should().Be(subcategoria.Id.ToString());
            Cleanup();
        }

        [Fact]
        public void Get_Categories_returns_object()
        {
            //Arrange
            var categoryList = new List<CategoriaDTO>();
            var existingCategory = new CategoriaDTO()
            {
                Id = dbContext.Categorias.FirstOrDefault().Id.ToString(),
                Descripcion = dbContext.Categorias.FirstOrDefault().Descripcion,
                Codigo = dbContext.Categorias.FirstOrDefault().Codigo,
                Area = dbContext.Categorias.FirstOrDefault().Id.ToString(),
                AreaDescripcion = dbContext.Categorias.FirstOrDefault().Descripcion,
                TipoCategoria = dbContext.Categorias.FirstOrDefault().TipoCategoria,
                Imagen = dbContext.Categorias.FirstOrDefault().Imagen,
                ImagenBase64 = dbContext.Categorias.FirstOrDefault().ImagenBase64
            };
            categoryList.Add(existingCategory);

            //Act
            var result = categoryRepository.GetCategories();

            //Assert
            result.Should().BeEquivalentTo(categoryList);
        }

        [Fact]
        public async void Get_Category_returns_object()
        {
            //Arrange
            var id = "84742078-7313-45BB-9357-407AA5CB6791";
            var existingCategory = dbContext.Categorias
                                    .FirstOrDefault(x =>
                                    x.Id.Equals(Guid.Parse(id)));

            //Act
            var result = await categoryRepository.GetCategory(id);

            //Assert
            result.Should().BeEquivalentTo(existingCategory);
        }

        [Fact]
        public async void Get_Subcategory_returns_object()
        {
            //Arrange
            var id = "C678D5F3-BFC1-4168-8214-5D064F6811A2";
            var existingCategory = dbContext.Subcategorias
                                    .FirstOrDefault(x =>
                                    x.Id.Equals(Guid.Parse(id)));

            //Act
            var result = await categoryRepository.GetSecondaryCategory(id);

            //Assert
            result.Should().BeEquivalentTo(existingCategory);
        }

        [Fact]
        public async void Get_MainCategories_returns_list()
        {
            //Arrange

            //Act
            var result = await categoryRepository.GetMainCategories();

            //Assert
            result.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public void Get_SecondaryCategories_returns_list()
        {
            //Arrange

            //Act
            var result = categoryRepository.GetSecondaryCategories(null);

            //Assert
            result.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public void Get_Subcategories_returns_object()
        {
            //Arrange
            var categoryList = new List<CategoriaDTO>();
            var existingCategory = new CategoriaDTO()
            {
                Id = dbContext.Subcategorias.FirstOrDefault().Id.ToString(),
                Descripcion = dbContext.Subcategorias.FirstOrDefault().Descripcion,
                Codigo = dbContext.Subcategorias.FirstOrDefault().Codigo,
                CategoriaPrincipal = dbContext.Subcategorias.FirstOrDefault().CategoriaPrincipal.ToString(),
                CategoriaPrincipalDescripcion = dbContext.Categorias.FirstOrDefault().Descripcion,
                TipoCategoria = dbContext.Subcategorias.FirstOrDefault().TipoCategoria,
                Imagen = dbContext.Subcategorias.FirstOrDefault().Imagen,
                ImagenBase64 = dbContext.Subcategorias.FirstOrDefault().ImagenBase64
            };
            categoryList.Add(existingCategory);

            //Act
            var result = categoryRepository.GetSubcategories();

            //Assert
            result.Should().BeEquivalentTo(categoryList);
        }

        [Fact]
        public async void Update_Category_returns_object()
        {
            //Arrange
            var categoria = new Categorias()
            {
                Id = dbContext.Categorias.FirstOrDefault().Id,
                Descripcion = "Test category",
                Area = dbContext.Areas.FirstOrDefault().Id,
                Imagen = null,
                ImagenBase64 = null,
                TipoCategoria = Models.Enums.TipoCategoria.Principal
            };

            //Act
            var result = await categoryRepository.UpdateCategory(categoria);

            //Assert
            result.Should().BeEquivalentTo(categoria);
        }

        [Fact]
        public async void Update_Subcategory_returns_object()
        {
            //Arrange
            var subcategoria = new Subcategorias()
            {
                Id = dbContext.Subcategorias.FirstOrDefault().Id,
                Descripcion = "Test subcategory",
                CategoriaPrincipal = dbContext.Categorias.FirstOrDefault().Id,
                Imagen = null,
                ImagenBase64 = null,
                TipoCategoria = Models.Enums.TipoCategoria.Secundario
            };

            //Act
            var result = await categoryRepository.UpdateSubcategory(subcategoria);

            //Assert
            result.Should().BeEquivalentTo(subcategoria);
        }

        [Fact]
        public void Get_Areas_returns_IQueryable()
        {
            //Arrange

            //Act
            var result = categoryRepository.GetAreas();

            //Assert
            result.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async void Update_Area_returns_object()
        {
            //Arrange
            var area = new Areas()
            {
                Id = Guid.Parse("6F490FC4-7AE6-44E4-89EC-320FECDE07C7"),
                Descripcion = "Test category",
                Imagen = null,
                ImagenBase64 = null,
                TipoCategoria = Models.Enums.TipoCategoria.Area
            };

            //Act
            var result = await categoryRepository.UpdateArea(area);

            //Assert
            result.Should().BeEquivalentTo(area);
        }

        [Fact]
        public void CreateImage_returns_true()
        {
            //Arrange
            var area = dbContext.Areas.FirstOrDefault().Id.ToString();
            var category = dbContext.Categorias.FirstOrDefault().Id.ToString();
            var subcategory = dbContext.Subcategorias.FirstOrDefault().Id.ToString();
            var id = "imageId";
            var base64Image = "base64";

            //Act
            var result = categoryRepository.CreateImage(id, area, base64Image);
            var result2 = categoryRepository.CreateImage(id, category, base64Image);
            var result3 = categoryRepository.CreateImage(id, subcategory, base64Image);

            //Assert
            result.Should().BeTrue();
            result2.Should().BeTrue();
            result3.Should().BeTrue();
        }

        [Fact]
        public async void GetCategoriesFromArea_returns_list()
        {
            //Arrange
            var areaId = dbContext.Areas.FirstOrDefault().Id.ToString();
            var categoriesCount = dbContext.Categorias
                                    .Where(x => x.Area.Equals(Guid.Parse(areaId))).Count();
            var categoriesList = new List<CategoriaDTO>();
            categoriesList.Add(new CategoriaDTO()
            {
                Id = dbContext.Categorias.FirstOrDefault().Id.ToString(),
                Descripcion = dbContext.Categorias.FirstOrDefault().Descripcion,
                Codigo = dbContext.Categorias.FirstOrDefault().Codigo,
                Area = dbContext.Categorias.FirstOrDefault().Area.ToString(),
                AreaDescripcion = dbContext.Categorias.FirstOrDefault().Descripcion,
                Imagen = dbContext.Categorias.FirstOrDefault().ImagenBase64,
                TipoCategoria = dbContext.Categorias.FirstOrDefault().TipoCategoria
            });

            //Act
            var result = await categoryRepository.GetCategoriesFromArea(areaId);

            //Assert
            result.Should().HaveCount(categoriesCount);
            result.Should().BeEquivalentTo(categoriesList);
        }

        [Fact]
        public async void GetSubcategoriesFromArea_returns_list()
        {
            //Arrange
            var categoryId = dbContext.Categorias.FirstOrDefault().Id.ToString();
            var categoriesCount = dbContext.Subcategorias
                     .Where(x => x.CategoriaPrincipal.Equals(Guid.Parse(categoryId))).Count();
            var categoriesList = new List<CategoriaDTO>();
            categoriesList.Add(new CategoriaDTO()
            {
                Id = dbContext.Subcategorias.FirstOrDefault().Id.ToString(),
                Descripcion = dbContext.Subcategorias.FirstOrDefault().Descripcion,
                Codigo = dbContext.Subcategorias.FirstOrDefault().Codigo,
                CategoriaPrincipal = dbContext.Subcategorias.FirstOrDefault().CategoriaPrincipal.ToString(),
                CategoriaPrincipalDescripcion = dbContext.Subcategorias.FirstOrDefault().Descripcion,
                Imagen = dbContext.Subcategorias.FirstOrDefault().ImagenBase64,
                TipoCategoria = dbContext.Subcategorias.FirstOrDefault().TipoCategoria
            });

            //Act
            var result = await categoryRepository.GetSubcategoriesFromCategory(categoryId);

            //Assert
            result.Should().HaveCount(categoriesCount);
            result.Should().BeEquivalentTo(categoriesList);
        }

        [TestCleanup]
        public virtual void Cleanup()
        {
            ServiceProvider.Dispose();
            ServiceProvider = null;
        }
    }
}
