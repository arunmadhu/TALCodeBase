using PremiumDomain.Model;
using System.Collections.Generic;
using System.Linq;

namespace PremiumDomain.Infrastructure.Repository
{
    /// <summary>
    /// Rating repository implementation
    /// </summary>
    public class RatingRepository : Repository<Rating>, IRatingRepository
    {
        /// <summary>
        /// Constructor for rating repository
        /// </summary>
        /// <param name="premiumDbContext">DB Context object</param>
        public RatingRepository(PremiumDbContext premiumDbContext) : base(premiumDbContext)
        {

        }

        /// <summary>
        /// Gets all ratings in the database
        /// </summary>
        /// <returns></returns>
        public IList<Rating> GetRatings()
        {
            return GetAll().ToList();
        }
    }
}
