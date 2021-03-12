using System;
using System.Collections.Generic;
using System.Text;

namespace PremiumDomain.Model
{
    public class Occupation
    {
        public int OccupationId { get; set; }
        public string OccupationName { get; set; }
        public int RatingId { get; set; }

        public virtual Rating Rating { get; set; }

    }
}
