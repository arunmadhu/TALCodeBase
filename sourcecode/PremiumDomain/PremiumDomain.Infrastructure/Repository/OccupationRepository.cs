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
        public OccupationRepository(PremiumDbContext premiumDbContext) : base(premiumDbContext)
        { 
        
        }

        public Occupation GetOccupationById(int occupationId)
        {
            return GetAll().FirstOrDefaultAsync(o => o.OccupationId == occupationId).Result;
        }

        public IList<Occupation> GetOccupations()
        {
            return GetAll().ToList();
        }
    }
}
