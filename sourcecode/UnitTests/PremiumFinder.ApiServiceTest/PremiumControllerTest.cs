using AutoFixture;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PremiumDomain.Model;
using PremiumDomain.Services;
using PremiumFinder.ApiServices.AutoMapper;
using PremiumFinder.ApiServices.Controllers;
using PremiumFinder.ApiServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PremiumFinder.ApiServiceTest
{
    public class Tests
    {
        private IFixture fixture;
        private Mock<IPremiumService> premiumService;

        private PremiumController premiumController;

        [SetUp]
        public void Setup()
        {
            fixture = new Fixture();
            premiumService = new Mock<IPremiumService>();

            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<PremiumApiProfile>();
            });
            var mapper = config.CreateMapper();

            premiumController = new PremiumController(premiumService.Object, mapper);
        }

        [Test]
        public void GetAllOccupationsTest()
        {
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var occupations = fixture.CreateMany<Occupation>(10).ToList();

            premiumService.Setup(p => p.GetOccupations()).Returns(occupations);

            var okResult = premiumController.GetAllOccupations() as OkObjectResult;
            var apiResult = okResult.Value as List<OccupationResponse>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            Assert.IsTrue(apiResult.Count == 10);
            Assert.AreEqual(apiResult.First().OccupationId, occupations.First().OccupationId);
        }

        [Test]
        public void GetPremiumTest()
        {
            var premiumResponse = new PremiumResponse();

            var apiRequest = fixture.Build<PremiumCalcRequest>()
                            .With(p => p.DateOfBirth, DateTime.Now.AddYears(-75))
                            .With(p => p.DeathSumInsured, 1000000)
                            .With(p => p.OccupationId, 4)
                            .With(p => p.UserName, "ASDF GHJK")
                            .Create();

            premiumService.Setup(ps => ps.CalculatePremium(It.Is<PremiumRequestView>(pr => pr.OccupationId.Equals(4))))
                                         .Callback((PremiumRequestView view) =>
                                         {
                                             premiumResponse.DeathPremium = 1000.50m;
                                             premiumResponse.RatingDesc = "Heavy Manual";
                                         });


            premiumController.GetPremium(apiRequest);

            Assert.AreEqual(premiumResponse.DeathPremium, 1000.50m);
            Assert.AreEqual(premiumResponse.RatingDesc, "Heavy Manual");
        }
    }
}