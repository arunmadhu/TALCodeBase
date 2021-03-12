using PremiumDomain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PremiumDomain.Infrastructure
{
    interface IRatingRepository
    {
        IList<Rating> GetRatings();
    }
}
