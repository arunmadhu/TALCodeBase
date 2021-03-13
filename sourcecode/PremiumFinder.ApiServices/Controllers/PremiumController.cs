using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PremiumDomain.Model;
using PremiumDomain.Services;
using PremiumFinder.ApiServices.Models;
using System.Collections.Generic;

namespace PremiumFinder.ApiServices.Controllers
{
    [Route("api/premium")]
    [ApiController]
    public class PremiumController : ControllerBase
    {
        private readonly IPremiumService _premiumService;
        private readonly IMapper _mapper;
        public PremiumController(IPremiumService premiumService, IMapper mapper)
        {
            _premiumService = premiumService;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("occupations")]
        public IActionResult GetAllOccupations()
        {
            var response = _mapper.Map<List<OccupationResponse>>(_premiumService.GetOccupations());
            return Ok(response);
        }

        [HttpPost]
        public IActionResult GetPremium(PremiumCalcRequest userData)
        {
            var premiumReq = _mapper.Map<PremiumRequestView>(userData);
            var response = _mapper.Map<PremiumResponse>(_premiumService.CalculatePremium(premiumReq));

            return Ok(response);
        }
    }
}
