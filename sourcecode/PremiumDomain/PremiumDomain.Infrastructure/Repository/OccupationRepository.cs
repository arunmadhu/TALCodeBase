using Microsoft.EntityFrameworkCore;
using PremiumDomain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumDomain.Infrastructure.Repository
{
    public class OccupationRepository : Repository<Occupation>,IOccupationRepository
    {
        private readonly PremiumDbContext _premiumDbContext;
        public OccupationRepository(PremiumDbContext premiumDbContext) : base(premiumDbContext)
        {
            _premiumDbContext = premiumDbContext;
        }

        public Occupation GetOccupationById(int occupationId)
        {
            var dbOccupation = _premiumDbContext.Occupations.Include(o => o.Rating).Where(o => o.OccupationId == occupationId).FirstOrDefault();
            return dbOccupation;
        }

        public IList<Occupation> GetOccupations()
        {
            return GetAll().ToList();
        }
    }
}
