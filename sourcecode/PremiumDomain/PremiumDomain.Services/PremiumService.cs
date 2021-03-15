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

        /// <summary>
        /// Premium Service constructor 
        /// </summary>
        /// <param name="occupationRepository">Occupation repository object</param>
        public PremiumService(IOccupationRepository occupationRepository)
        {
            _occupationRepository = occupationRepository;
        }

        /// <summary>
        /// Repo service which calculates the premium based on user inputs
        /// </summary>
        /// <param name="premiumRequest">User provided information</param>
        /// <returns>View object with premium details <returns>
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
                response.Age = age;
            }

            return response;
        }

        /// <summary>
        /// Repo Service returning all occupations
        /// </summary>
        /// <returns></returns>
        public IList<Occupation> GetOccupations()
        {
            return _occupationRepository.GetOccupations();
        }
    }
}
