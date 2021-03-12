using PremiumDomain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PremiumDomain.Infrastructure 
{
    interface IOccupationRepository
    {
        IList<Occupation> GetOccupations();

        Task<Occupation> GetOccupationByIdAsync(int occupationId);
    }
}
