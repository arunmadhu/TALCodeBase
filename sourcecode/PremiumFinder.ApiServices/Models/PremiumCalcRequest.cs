using PremiumFinder.ApiServices.Models.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PremiumFinder.ApiServices.Models
{
    public class PremiumCalcRequest
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Only alphabets allowed")]
        public string UserName { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public int OccupationId { get; set; }


        [Required]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DOBValidation(ErrorMessage = "Date of birth cannot be greater than current date")]
        public System.DateTime DateOfBirth { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Death Sum Insured should be greater than 0")]
        public decimal DeathSumInsured { get; set; }
    }
}
