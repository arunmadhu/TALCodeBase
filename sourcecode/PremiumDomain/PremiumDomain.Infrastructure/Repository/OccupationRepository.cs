using Microsoft.EntityFrameworkCore;
using PremiumDomain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumDomain.Infrastructure.Repository
{
    /// <summary>
    /// Occupation repository implementation
    /// </summary>
    public class OccupationRepository : Repository<Occupation>,IOccupationRepository
    {
        private readonly PremiumDbContext _premiumDbContext;
        public OccupationRepository(PremiumDbContext premiumDbContext) : base(premiumDbContext)
        {
            _premiumDbContext = premiumDbContext;
        }

        /// <summary>
        /// Gets occupation details by id
        /// </summary>
        /// <param name="occupationId">Primary key of occupation</param>
        /// <returns>Occpation entity</returns>
        public Occupation GetOccupationById(int occupationId)
        {
            var dbOccupation = _premiumDbContext.Occupations.Include(o => o.Rating).Where(o => o.OccupationId == occupationId).FirstOrDefault();
            return dbOccupation;
        }

        /// <summary>
        /// Gets all occupations in database
        /// </summary>
        /// <returns>Occupation entity list</returns>
        public IList<Occupation> GetOccupations()
        {
            return GetAll().ToList();
        }
    }
}
