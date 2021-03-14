using PremiumDomain.Infrastructure;
using PremiumDomain.Model;

namespace WebApi.xUnitIntegrationTest
{
    public static class SeedData
    {
        public static void PopulateTestData(PremiumDbContext dbContext)
        {
            dbContext.Occupations.Add(new Occupation { OccupationId = 1, OccupationName = "Cleaner" });
            dbContext.Occupations.Add(new Occupation { OccupationId = 2, OccupationName = "Doctor" });

            dbContext.SaveChanges();
        }
    }
}
