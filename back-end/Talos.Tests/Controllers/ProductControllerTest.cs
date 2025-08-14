using AutoMapper;
using Domain.Interfaces;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
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
    public class ProductControllerTest
    {
        private UserManager<ApplicationUser> userManager;
        private IMapper mapper;
        private IProductRepository productRepository;
        private IDriveRepository driveRepository;
        private ProductsHelper productHelper;
        private ProductController productController;


        public ProductControllerTest()
        {
            this.userManager = A.Fake<UserManager<ApplicationUser>>();
            this.mapper = A.Fake<IMapper>();
            this.productRepository = A.Fake<IProductRepository>();
            this.driveRepository = A.Fake<IDriveRepository>();
            this.productHelper = new ProductsHelper(driveRepository);

            //Initialize controller
            this.productController = new ProductController(userManager, mapper, productRepository,driveRepository);
        }

        [Fact]
        public void GetArea_returns_Ok()
        {
            //Arrange
            

            //Act
            

            //Assert
            
        }
    }
}
