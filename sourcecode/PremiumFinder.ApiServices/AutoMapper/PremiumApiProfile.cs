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

            CreateMap<PremiumCalcRequest, PremiumRequestView>()
                .ForMember(map => map.OccupationId, des => des.MapFrom(src => src.OccupationId))
                .ForMember(map => map.SumInsured, des => des.MapFrom(src => src.DeathSumInsured))
                .ForMember(map => map.DateOfBirth, des => des.MapFrom(src => src.DateOfBirth));
        }
    }
}
