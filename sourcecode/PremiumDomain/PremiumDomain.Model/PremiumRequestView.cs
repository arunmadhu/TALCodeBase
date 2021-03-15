using System;
using System.Collections.Generic;
using System.Text;

namespace PremiumDomain.Model
{
    public class PremiumRequestView
    {
        public decimal SumInsured { get; set; }
        public int OccupationId { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
