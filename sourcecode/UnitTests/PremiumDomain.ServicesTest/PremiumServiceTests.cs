using AutoFixture;
using AutoMapper;
using Moq;
using NUnit.Framework;
using PremiumDomain.Infrastructure;
using PremiumDomain.Model;
using PremiumDomain.Services;
using PremiumFinder.ApiServices.AutoMapper;
using System;
using System.Linq;

namespace PremiumDomain.ServicesTest
{
    public class Tests
    {
        private IFixture fixture;
        private Mock<IOccupationRepository> occupationRepository;

        private PremiumService premiumService;

        [SetUp]
        public void Setup()
        {
            fixture = new Fixture();
            occupationRepository = new Mock<IOccupationRepository>();

            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<PremiumApiProfile>();
            });
            var mapper = config.CreateMapper();

            premiumService = new PremiumService(occupationRepository.Object);
        }

        [Test]
        public void GetOccupationsTest()
        {
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var occupations = fixture.Build<Occupation>().CreateMany(10);

            occupationRepository.Setup(o => o.GetOccupations()).Returns(occupations.ToList());

            var response = premiumService.GetOccupations();

            Assert.IsNotNull(response);
            Assert.AreEqual(response.Count, 10);
        }

        [Test]
        public void CalculatePremiumTest()
        {
            var repoResponse = new Occupation();
            repoResponse.Rating = new Rating { Factor = 1.75m, RatingName = "Heavy Manual" };

            var serviceRequest = fixture.Build<PremiumRequestView>()
                                    .With(pq => pq.DateOfBirth, DateTime.Now.AddYears(-75))
                                    .With(pq => pq.OccupationId, 4)
                                    .With(pq => pq.SumInsured, 1000000)
                                    .Create();

            occupationRepository.Setup(o => o.GetOccupationById(It.Is<int>(o => o == 4))).Returns(repoResponse);


            var response = premiumService.CalculatePremium(serviceRequest);

            Assert.IsNotNull(response);
            Assert.AreEqual(response.DeathPremium, 1575000.00M);
            Assert.AreEqual(response.Rating, "Heavy Manual");
        }
    }
}