using PremiumDomain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PremiumDomain.Infrastructure
{
    /// <summary>
    /// Rating repository definitions
    /// </summary>
    interface IRatingRepository
    {
        IList<Rating> GetRatings();
    }
}
