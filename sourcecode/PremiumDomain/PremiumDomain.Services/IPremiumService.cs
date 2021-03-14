using PremiumDomain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PremiumDomain.Services
{
    /// <summary>
    /// Premium Service interface exposing the domain services.
    /// </summary>
    public interface IPremiumService
    {
        IList<Occupation> GetOccupations();

        PremiumView CalculatePremium(PremiumRequestView premiumRequest);
    }
}
