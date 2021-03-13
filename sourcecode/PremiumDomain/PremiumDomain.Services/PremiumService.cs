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

        public PremiumView CalculatePremium(int sumInsured, int occupationId, DateTime dateOfBirth)
        {
            decimal deathPremium;
            var response = new PremiumView();
            var age = DateTime.Now.Year - dateOfBirth.Year;

            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age--;

            var occupation =  _occupationRepository.GetOccupationById(occupationId);

            if (occupation != null)
            {
                deathPremium = ((sumInsured * occupation.Rating.Factor * age) / 1000) * 12;

                response.DeathPremium = Math.Round(deathPremium, 2);
                response.Rating = occupation.Rating.RatingName;
            }

            return response;
        }

        public IList<Occupation> GetOccupations()
        {
            return _occupationRepository.GetOccupations();
        }
    }
}
