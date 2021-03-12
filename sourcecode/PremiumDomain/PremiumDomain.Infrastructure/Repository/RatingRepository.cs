using PremiumDomain.Model;
using System.Collections.Generic;
using System.Linq;

namespace PremiumDomain.Infrastructure.Repository
{
    public class RatingRepository : Repository<Rating>, IRatingRepository
    {
        public RatingRepository(PremiumDbContext premiumDbContext) : base(premiumDbContext)
        {

        }

        public IList<Rating> GetRatings()
        {
            return GetAll().ToList();
        }
    }
}
