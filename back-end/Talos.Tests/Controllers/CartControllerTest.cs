using AutoMapper;
using Domain.Interfaces;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Classes;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talos.API.Classes;
using Talos.API.Controllers;
using Talos.API.User;
using Xunit;

namespace Talos.Tests.Controllers
{
    public class CartControllerTest
    {
        private UserManager<ApplicationUser> userManager;
        private IMapper mapper;
        private ICartRepository cartRepository;
        private IDriveRepository driveRepository;
        private CartController cartController;


        public CartControllerTest()
        {
            this.userManager = A.Fake<UserManager<ApplicationUser>>();
            this.mapper = A.Fake<IMapper>();
            this.cartRepository = A.Fake<ICartRepository>();
            this.driveRepository = A.Fake<IDriveRepository>();

            //Initialize controller
            cartController = new CartController(userManager, mapper, cartRepository);
        }

        [Fact]
        public async void GetCart_returns_Ok()
        {
            //Arrange
            var id = "1";
            var cartDTO = new CarritoDTO()
            {
                Id = Guid.NewGuid().ToString()
            };
            A.CallTo(() => cartRepository.GetCart(id)).Returns(cartDTO);

            //Act
            var result = await cartController.GetCart(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async void CreateCart_returns_Ok_BadRequest()
        {
            //Arrange
            List<Carrito> cart = new List<Carrito>();
            List<CarritoDTO> cartDTO = new List<CarritoDTO>();
            List<CarritoDTO> cartDTO2 = new List<CarritoDTO>();
            cart.Add(new Carrito()
            {
                Id = Guid.NewGuid(),
                SesionId = Guid.NewGuid(),
                ProductoId = Guid.NewGuid(),
                Cantidad = 2,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            });
            cartDTO.Add(new CarritoDTO()
            {
                Id = Guid.NewGuid().ToString(),
                SesionId = null,
                ProductoId = Guid.NewGuid(),
                Cantidad = 2,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now,
                Email = "richo84@gmail.com"
            });
            cartDTO2.Add(new CarritoDTO()
            {
                Id = Guid.NewGuid().ToString(),
                SesionId = null,
                ProductoId = null,
                Cantidad = 2,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now,
                Email = "richo84@gmail.com"
            });
            var sesion = A.Fake<Sesion>();
            A.CallTo(() => mapper.Map<List<CarritoDTO>, List<Carrito>>(cartDTO)).Returns(cart);
            A.CallTo(() => cartRepository.CreateSesion(cart[0], cartDTO[0].Email)).Returns(sesion);
            A.CallTo(() => cartRepository.CreateCart(cart)).Returns(1);

            //Act
            var result = await cartController.CreateCart(cartDTO);
            var result2 = await cartController.CreateCart(cartDTO2);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            result2.Should().BeOfType(typeof(BadRequestObjectResult));
        }

        [Fact]
        public async void UpdateCart_returns_Ok_BadRequest()
        {
            //Arrange
            List<Carrito> cart = new List<Carrito>();
            List<CarritoDTO> cartDTO = new List<CarritoDTO>();
            List<CarritoDTO> cartDTO2 = new List<CarritoDTO>();
            cart.Add(new Carrito()
            {
                Id = Guid.NewGuid(),
                SesionId = Guid.NewGuid(),
                ProductoId = Guid.NewGuid(),
                Cantidad = 2,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            });
            cartDTO.Add(new CarritoDTO()
            {
                Id = Guid.NewGuid().ToString(),
                SesionId = null,
                ProductoId = Guid.NewGuid(),
                Cantidad = 2,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now,
                Email = "richo84@gmail.com"
            });
            cartDTO2.Add(new CarritoDTO()
            {
                Id = null,
                SesionId = null,
                ProductoId = Guid.NewGuid(),
                Cantidad = 2,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now,
                Email = "richo84@gmail.com"
            });
            var sesion = A.Fake<Sesion>();
            A.CallTo(() => mapper.Map<List<CarritoDTO>, List<Carrito>>(cartDTO)).Returns(cart);
            A.CallTo(() => cartRepository.CreateSesion(cart[0], cartDTO[0].Email)).Returns(sesion);
            A.CallTo(() => cartRepository.UpdateCart(cart)).Returns(cart);

            //Act
            var result = await cartController.UpdateCart(cartDTO);
            var result2 = await cartController.UpdateCart(cartDTO2);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            result2.Should().BeOfType(typeof(BadRequestObjectResult));
        }

        [Fact]
        public async void DeleteCart_returns_Ok()
        {
            //Arrange
            var id = "1";
            A.CallTo(() => cartRepository.DeleteCart(id)).Returns("Operación exitosa");

            //Act
            var result = await cartController.DeleteCart(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async void DeleteCart_returns_BadRequest()
        {
            //Arrange
            var id = "1";
            A.CallTo(() => cartRepository.DeleteCart(id)).Returns("Ocurrió un error al eliminar el carrito");

            //Act
            var result = await cartController.DeleteCart(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(BadRequestObjectResult));
        }
    }
}
