using Domain.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Domain.DomainRepositories;
using FluentAssertions;
using FakeItEasy;
using AutoMapper;
using Models.Classes;
using Models.DTOs;
using System.Collections.Generic;

namespace Talos.Tests.Repositories
{
    [TestClass]
    public class CampaignRepositoryTests
    {
        public ServiceCollection Services { get; private set; }
        public ServiceProvider ServiceProvider { get; protected set; }
        private CampaignRepository _campaignRepository;
        private ApplicationDbContext _dbContext;
        private IMapper _mapper;

        public CampaignRepositoryTests()
        {
            Initialize();
            _dbContext = ServiceProvider.GetService<ApplicationDbContext>();
            _mapper = A.Fake<IMapper>();
            _campaignRepository = new CampaignRepository(_dbContext,_mapper);
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

            dbContext.Campañas.Add(new Models.Classes.Campañas
            {
                Id = Guid.Parse("832DF7D6-9346-44FE-B5C1-DAEAB46DBF3A"),
                Descripcion = "Descuento especial de lanzamiento",
                Estado = true,
                FechaCreacion = DateTime.Now,
                FechaEdicion = DateTime.Now,
                FechaInicioVigencia = DateTime.Now.AddDays(-1),
                FechaFinVigencia = DateTime.Now.AddDays(10),
                Nombre = "Lanzamiento",
                PorcentajeDescuento = 20
            });

            dbContext.CampaignProducts.Add(new CampaignProducts
            {
                Id = Guid.NewGuid(),
                CampaignId = Guid.Parse("832DF7D6-9346-44FE-B5C1-DAEAB46DBF3A"),
                ProductId = Guid.Parse("832DF7D6-9341-45FE-B5C1-DAEAB46DBF3A")
            });

            dbContext.SaveChanges();
        }

        [Fact]
        public async void CreateCampaign_Returns_true()
        {
            //Arrange
            var existingCampaign = _dbContext.Campañas.FirstOrDefault();
            var newCampaign = new Campañas()
            {
                Id = Guid.NewGuid(),
                Descripcion = "Nueva campaña de prueba",
                Nombre = "Nueva campaña",
                FechaCreacion = DateTime.Now,
                FechaEdicion = DateTime.Now,
                Estado=true,
                FechaInicioVigencia = DateTime.Now.AddDays(-2),
                FechaFinVigencia = DateTime.Now.AddDays(5)
            };

            //Act
            var result = await _campaignRepository.CreateCampaign(newCampaign);

            //Assert
            _dbContext.Campañas.Count().Should().Be(2);
            result.FechaCreacion.Should().Be(newCampaign.FechaCreacion);
            result.Should().NotBe(existingCampaign);
            
            Cleanup();
        }

        [Fact]
        public async void DeleteCampaign_returns_success()
        {
            //Arrange
            var newCampaign = new Campañas()
            {
                Id = Guid.NewGuid(),
                Descripcion = "Nueva campaña de prueba",
                Nombre = "Nueva campaña",
                FechaCreacion = DateTime.Now,
                FechaEdicion = DateTime.Now,
                Estado = true,
                FechaInicioVigencia = DateTime.Now.AddDays(-2),
                FechaFinVigencia = DateTime.Now.AddDays(5)
            };

            //Act
            var create = await _campaignRepository.CreateCampaign(newCampaign);
            var result = await _campaignRepository.DeleteCampaign(newCampaign.Id.ToString());

            //Assert
            _dbContext.Campañas.Count().Should().Be(1);
            result.Should().Be("Operación exitosa");

            Cleanup();
        }

        [Fact]
        public void GetAllDiscounts_returns_list()
        {
            //Arrange

            //Act
            var result = _campaignRepository.GetAllCampaigns();

            //Assert
            result.Should().HaveCountGreaterThanOrEqualTo(1);

            Cleanup();
        }

        [Fact]
        public async void GetCampaign_returns_Campañas()
        {
            //Arrange
            var id = "832DF7D6-9346-44FE-B5C1-DAEAB46DBF3A";
            var campaign = _dbContext.Campañas.FirstOrDefault();
            var campaignDTO = new CampaignDTO()
            {
                Id = id,
                Descripcion = campaign.Descripcion,
                Estado = campaign.Estado,
                FechaCreacion = campaign.FechaCreacion.Value,
                FechaEdicion = campaign.FechaEdicion.Value,
                Nombre = campaign.Nombre,
                PorcentajeDescuento = campaign.PorcentajeDescuento
            };
            A.CallTo(() => _mapper.Map<Campañas, CampaignDTO>(campaign)).Returns(campaignDTO);

            //Act
            var result = await _campaignRepository.GetCampaign(id);

            //Assert
            result.Should().BeOfType(typeof(CampaignDTO));
            result.Descripcion.Should().NotBeNull();

            Cleanup();
        }

        [Fact]
        public void GetCampaigns_returns_list()
        {
            //Arrange

            //Act
            var result = _campaignRepository.GetCampaigns();

            //Assert
            result.Should().HaveCountGreaterThan(0);

            Cleanup();
        }

        [Fact]
        public async void UpdateDiscount_returns_Descuento()
        {
            //Arrange
            var newCampaign = new Campañas()
            {
                Id = Guid.Parse("832DF7D6-9346-44FE-B5C1-DAEAB46DBF3A"),
                Descripcion = "Nueva campaña de prueba 2",
                Nombre = "Nueva campaña",
                FechaCreacion = DateTime.Now,
                FechaEdicion = DateTime.Now,
                Estado = true,
                FechaInicioVigencia = DateTime.Now.AddDays(-2),
                FechaFinVigencia = DateTime.Now.AddDays(5)
            };

            //Act
            var result = await _campaignRepository.UpdateCampaign(newCampaign);

            //Assert
            result.Descripcion.Should().Be("Nueva campaña de prueba 2");

            Cleanup();
        }

        [Fact]
        public void GetAllProducts_returns_list()
        {
            //Arrange

            //Act
            var result = _campaignRepository.GetAllProducts();

            //Assert
            result.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public void InsertCampaignProducts_returns_true()
        {
            //Arrange
            var products = new List<string>();
            products.Add("832DF7D6-9341-45FE-B5C1-DAEAB46DBF3A");
            var id = Guid.Parse("832DF7D6-9346-44FE-B5C1-DAEAB46DBF3A");

            //Act
            var result = _campaignRepository.InsertCampaignProducts(products, id);

            //Assert
            result.Should().BeTrue();
            _dbContext.CampaignProducts.Count().Should().Be(1);
        }

        [TestCleanup]
        public virtual void Cleanup()
        {
            ServiceProvider.Dispose();
            ServiceProvider = null;
        }
    }
}
