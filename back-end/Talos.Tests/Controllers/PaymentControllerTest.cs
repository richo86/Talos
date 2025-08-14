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
    public class PaymentControllerTest
    {
        private UserManager<ApplicationUser> userManager;
        private IMapper mapper;
        private IPaymentRepository paymentRepository;
        private PaymentController paymentController;


        public PaymentControllerTest()
        {
            this.userManager = A.Fake<UserManager<ApplicationUser>>();
            this.mapper = A.Fake<IMapper>();
            this.paymentRepository = A.Fake<IPaymentRepository>();

            //Initialize controller
            paymentController = new PaymentController(userManager, mapper, paymentRepository);
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
