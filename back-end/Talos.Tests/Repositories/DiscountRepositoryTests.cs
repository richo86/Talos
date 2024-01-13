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

namespace Talos.Tests.Repositories
{
    [TestClass]
    public class DiscountRepositoryTests
    {
        public ServiceCollection Services { get; private set; }
        public ServiceProvider ServiceProvider { get; protected set; }
        private DiscountRepository _discountRepository;
        private ApplicationDbContext _dbContext;
        private IMapper _mapper;

        public DiscountRepositoryTests()
        {
            Initialize();
            _dbContext = ServiceProvider.GetService<ApplicationDbContext>();
            _mapper = A.Fake<IMapper>();
            _discountRepository = new DiscountRepository(_dbContext,_mapper);
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

            dbContext.Descuentos.Add(new Models.Classes.Descuentos
            {
                Id = Guid.Parse("832DF7D6-9346-44FE-B5C1-DAEAB46DBF3A"),
                CodigoPromocion = "PROMO2023",
                Descripcion = "Descuento especial de lanzamiento",
                Estado = true,
                FechaCreacion = DateTime.Now,
                FechaEdicion = DateTime.Now,
                FechaInicioVigencia = DateTime.Now.AddDays(-1),
                FechaFinVigencia = DateTime.Now.AddDays(10),
                Nombre = "Lanzamiento",
                PorcentajeDescuento = 20
            });

            dbContext.SaveChanges();
        }

        [Fact]
        public async void CreateDiscount_Returns_true()
        {
            //Arrange
            var existingDiscount = _dbContext.Descuentos.FirstOrDefault();
            var newDiscount = new Descuentos()
            {
                Id = Guid.NewGuid(),
                Descripcion = "Nueva campaña de prueba",
                Nombre = "Nueva campaña",
                CodigoPromocion = null,
                FechaCreacion = DateTime.Now,
                FechaEdicion = DateTime.Now,
                Estado=true,
                FechaInicioVigencia = DateTime.Now.AddDays(-2),
                FechaFinVigencia = DateTime.Now.AddDays(5)
            };

            //Act
            var result = await _discountRepository.CreateDiscount(newDiscount);

            //Assert
            _dbContext.Descuentos.Count().Should().Be(2);
            result.FechaCreacion.Should().Be(newDiscount.FechaCreacion);
            result.Should().NotBe(existingDiscount);
            
            Cleanup();
        }

        [Fact]
        public async void DeleteDiscount_returns_success()
        {
            //Arrange
            var newDiscount = new Descuentos()
            {
                Id = Guid.NewGuid(),
                Descripcion = "Nueva campaña de prueba",
                Nombre = "Nueva campaña",
                CodigoPromocion = null,
                FechaCreacion = DateTime.Now,
                FechaEdicion = DateTime.Now,
                Estado = true,
                FechaInicioVigencia = DateTime.Now.AddDays(-2),
                FechaFinVigencia = DateTime.Now.AddDays(5)
            };

            //Act
            var create = await _discountRepository.CreateDiscount(newDiscount);
            var result = await _discountRepository.DeleteDiscount(newDiscount.Id.ToString());

            //Assert
            _dbContext.Descuentos.Count().Should().Be(1);
            result.Should().Be("Operación exitosa");

            Cleanup();
        }

        [Fact]
        public void GetAllDiscounts_returns_list()
        {
            //Arrange

            //Act
            var result = _discountRepository.GetAllDiscounts();

            //Assert
            result.Should().HaveCountGreaterThanOrEqualTo(1);

            Cleanup();
        }

        [Fact]
        public async void GetDiscount_returns_Descuentos()
        {
            //Arrange
            var id = "832DF7D6-9346-44FE-B5C1-DAEAB46DBF3A";
            var discount = _dbContext.Descuentos.FirstOrDefault();
            var discountDTO = new DescuentoDTO()
            {
                Id = id,
                Descripcion = discount.Descripcion,
                Estado = discount.Estado,
                FechaCreacion = discount.FechaCreacion.Value,
                FechaEdicion = discount.FechaEdicion.Value,
                Nombre = discount.Nombre,
                PorcentajeDescuento = discount.PorcentajeDescuento
            };
            A.CallTo(() => _mapper.Map<Descuentos, DescuentoDTO>(discount)).Returns(discountDTO);

            //Act
            var result = await _discountRepository.GetDiscount(id);

            //Assert
            result.Should().BeOfType(typeof(DescuentoDTO));
            result.Descripcion.Should().NotBeNull();

            Cleanup();
        }

        [Fact]
        public void GetDiscounts_returns_list()
        {
            //Arrange

            //Act
            var result = _discountRepository.GetDiscounts();

            //Assert
            result.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async void UpdateDiscount_returns_Descuento()
        {
            //Arrange
            var newDiscount = new Descuentos()
            {
                Id = Guid.Parse("832DF7D6-9346-44FE-B5C1-DAEAB46DBF3A"),
                Descripcion = "Nueva campaña de prueba 2",
                Nombre = "Nueva campaña",
                CodigoPromocion = "codigo",
                FechaCreacion = DateTime.Now,
                FechaEdicion = DateTime.Now,
                Estado = true,
                FechaInicioVigencia = DateTime.Now.AddDays(-2),
                FechaFinVigencia = DateTime.Now.AddDays(5)
            };

            //Act
            var result = await _discountRepository.UpdateDiscount(newDiscount);

            //Assert
            result.CodigoPromocion.Should().Be("codigo");

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
