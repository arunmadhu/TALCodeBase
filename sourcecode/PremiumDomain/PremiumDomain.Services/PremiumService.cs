using PremiumDomain.Infrastructure;
using PremiumDomain.Infrastructure.Repository;
using PremiumDomain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PremiumDomain.Services
{
    public class PremiumService : IPremiumService
    {
        private readonly IOccupationRepository _occupationRepository;
        public PremiumService(IOccupationRepository occupationRepository)
        {
            _occupationRepository = occupationRepository;
        }

        public PremiumView CalculatePremium(PremiumRequestView premiumRequest)
        {
            decimal deathPremium;
            var response = new PremiumView();
            var age = DateTime.Now.Year - premiumRequest.DateOfBirth.Year;

            if (DateTime.Now.DayOfYear < premiumRequest.DateOfBirth.DayOfYear)
                age--;

            var occupation =  _occupationRepository.GetOccupationById(premiumRequest.OccupationId);

            if (occupation != null)
            {
                deathPremium = ((premiumRequest.SumInsured * occupation.Rating.Factor * age) / 1000) * 12;

                response.DeathPremium = Math.Round(deathPremium, 2);
                response.RatingDesc = occupation.Rating.RatingName;
            }

            return response;
        }

        public IList<Occupation> GetOccupations()
        {
            return _occupationRepository.GetOccupations();
        }
    }
}
