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
    public class StoreFrontControllerTest
    {
        private UserManager<ApplicationUser> userManager;
        private IMapper mapper;
        private IStoreFrontRepository storeFrontRepository;
        private IDriveRepository driveRepository;
        private StoreFrontController storeFrontController;


        public StoreFrontControllerTest()
        {
            this.userManager = A.Fake<UserManager<ApplicationUser>>();
            this.mapper = A.Fake<IMapper>();
            this.storeFrontRepository = A.Fake<IStoreFrontRepository>();
            this.driveRepository = A.Fake<IDriveRepository>();

            //Initialize controller
            storeFrontController = new StoreFrontController(userManager, mapper, storeFrontRepository,driveRepository);
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
