using PremiumDomain.Infrastructure;
using PremiumDomain.Model;

namespace WebApi.IntegrationTest
{
    public static class SeedData
    {
        public static void PopulateTestData(PremiumDbContext dbContext)
        {
            //dbContext.occ.Add(new Player("Wayne", "Gretzky", 183, 84, new DateTime(1961, 1, 26)) { Id = 1, Created = DateTime.UtcNow });
            //dbContext.Players.Add(new Player("Mario", "Lemieux", 193, 91, new DateTime(1965, 11, 5)) { Id = 2, Created = DateTime.UtcNow });

            dbContext.Occupations.Add(new Occupation {OccupationId = 1,OccupationName = "Cleaner" });
            dbContext.Occupations.Add(new Occupation {OccupationId = 2,OccupationName = "Doctor" });

            dbContext.SaveChanges();
        }
    }
}
