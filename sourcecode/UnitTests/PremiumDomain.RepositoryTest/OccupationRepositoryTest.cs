using AutoFixture;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using PremiumDomain.Infrastructure;
using PremiumDomain.Infrastructure.Repository;
using PremiumDomain.Model;
using PremiumFinder.ApiServices.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PremiumDomain.RepositoryTest
{
    public class OccupationRepositoryTest
    {
        private IFixture fixture;
        private Mock<PremiumDbContext> premiumDbContext;

        private OccupationRepository occupationRepository;

        [SetUp]
        public void Setup()
        {
            fixture = new Fixture();
            premiumDbContext = new Mock<PremiumDbContext>();

            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<PremiumApiProfile>();
            });
            var mapper = config.CreateMapper();

            occupationRepository = new OccupationRepository(premiumDbContext.Object);
        }

        [Test]
        public void GetOccupationsTest()
        {
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var occupations = fixture.Build<Occupation>().CreateMany(10);

            var occupationMock = new Mock<DbSet<Occupation>>();

            occupationMock.As<IQueryable<Occupation>>().Setup(m => m.GetEnumerator()).Returns(occupations.GetEnumerator());

            premiumDbContext.Setup(db => db.Set<Occupation>()).Returns(occupationMock.Object);
            premiumDbContext.Setup(db => db.Occupations).Returns(occupationMock.Object);

            var repoResponse = occupationRepository.GetOccupations();

            Assert.IsNotNull(repoResponse);
            Assert.AreEqual(repoResponse.Count(), 10);
        }
    }
}
