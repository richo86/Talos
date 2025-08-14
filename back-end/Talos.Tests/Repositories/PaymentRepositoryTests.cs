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
using Talos.API.User;
using Microsoft.AspNetCore.Identity;
using FakeItEasy;

namespace Talos.Tests.Repositories
{
    [TestClass]
    public class PaymentRepositoryTests
    {
        public ServiceCollection Services { get; private set; }
        public ServiceProvider ServiceProvider { get; protected set; }
        private PaymentRepository paymentRepository;
        private ApplicationDbContext dbContext;
        private UserManager<ApplicationUser> userManager;

        public PaymentRepositoryTests()
        {
            Initialize();
            this.dbContext = ServiceProvider.GetService<ApplicationDbContext>();
            this.userManager = A.Fake<UserManager<ApplicationUser>>();
            this.paymentRepository = new PaymentRepository(dbContext,userManager);
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

            dbContext.SaveChanges();
        }

        [Fact]
        public void Create_Image_Returns_true()
        {
            //Arrange


            //Act


            //Assert

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
