using AutoFixture;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using PremiumDomain.Infrastructure;
using PremiumDomain.Infrastructure.Repository;
using PremiumDomain.Model;
using PremiumFinder.ApiServices.AutoMapper;
using System.Linq;

namespace PremiumDomain.RepositoryTest
{
    public class RatingRepositoryTest
    {
        private IFixture fixture;
        private Mock<PremiumDbContext> premiumDbContext;

        private RatingRepository ratingRepository;

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

            ratingRepository = new RatingRepository(premiumDbContext.Object);
        }

        [Test]
        public void GetRatingsTest()
        {
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var ratings = fixture.Build<Rating>().CreateMany(10);

            var ratingMock = new Mock<DbSet<Rating>>();

            ratingMock.As<IQueryable<Rating>>().Setup(m => m.GetEnumerator()).Returns(ratings.GetEnumerator());

            premiumDbContext.Setup(db => db.Set<Rating>()).Returns(ratingMock.Object);
            premiumDbContext.Setup(db => db.Ratings).Returns(ratingMock.Object);

            var repoResponse = ratingRepository.GetRatings();

            Assert.IsNotNull(repoResponse);
            Assert.AreEqual(repoResponse.Count(), 10);
        }
    }
}