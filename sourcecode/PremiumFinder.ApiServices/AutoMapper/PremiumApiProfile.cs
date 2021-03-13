using AutoMapper;
using PremiumDomain.Model;
using PremiumFinder.ApiServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PremiumFinder.ApiServices.AutoMapper
{
    public class PremiumApiProfile : Profile
    {
        public PremiumApiProfile()
        {
            CreateMap<Occupation, OccupationResponse>();
            CreateMap<PremiumView, PremiumResponse>();
        }
    }
}
