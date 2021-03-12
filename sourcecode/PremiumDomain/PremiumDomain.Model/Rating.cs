using System;
using System.Collections.Generic;
using System.Text;

namespace PremiumDomain.Model
{
    public partial class Rating
    {
        public Rating()
        {
            Occupations = new HashSet<Occupation>();
        }

        public int RatingId { get; set; }
        public string RatingName { get; set; }
        public decimal Factor { get; set; }

        public virtual ICollection<Occupation> Occupations { get; set; }

    }
}
