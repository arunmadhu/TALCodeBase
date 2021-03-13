using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PremiumFinder.ApiServices.Models
{
    public class PremiumResponse
    {
        public string RatingDesc { get; set; }

        public decimal DeathPremium { get; set; }
    }
}
