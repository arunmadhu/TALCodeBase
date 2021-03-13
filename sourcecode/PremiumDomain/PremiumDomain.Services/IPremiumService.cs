using PremiumDomain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PremiumDomain.Services
{
    public interface IPremiumService
    {
        IList<Occupation> GetOccupations();

        PremiumView CalculatePremium(PremiumRequestView premiumRequest);
    }
}
