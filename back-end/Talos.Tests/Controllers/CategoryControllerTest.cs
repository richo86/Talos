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
    public class CategoryControllerTest
    {
        private UserManager<ApplicationUser> userManager;
        private IMapper mapper;
        private ICategoryRepository categoryRepository;
        private IDriveRepository driveRepository;
        private CategoriesHelper categoryHelper;
        private CategoryController categoryController;


        public CategoryControllerTest()
        {
            this.userManager = A.Fake<UserManager<ApplicationUser>>();
            this.mapper = A.Fake<IMapper>();
            this.categoryRepository = A.Fake<ICategoryRepository>();
            this.driveRepository = A.Fake<IDriveRepository>();
            this.categoryHelper = new CategoriesHelper(driveRepository);

            //Initialize controller
            categoryController = new CategoryController(userManager, mapper, categoryRepository,driveRepository);
        }

        [Fact]
        public async void GetArea_returns_Ok()
        {
            //Arrange
            var id = "1";
            var area = new Areas()
            {
                Descripcion = "something"
            };
            A.CallTo(() => categoryRepository.GetArea(id)).Returns(area);
            var categoriaDTO = A.Fake<CategoriaDTO>();
            A.CallTo(() => mapper.Map<Areas, CategoriaDTO>(area)).Returns(categoriaDTO);
            var category = A.Fake<CategoriaDTO>();
            A.CallTo(() => driveRepository.GetFileById(category.Imagen)).Returns("base64");

            //Act
            var result = await categoryController.GetArea(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}
