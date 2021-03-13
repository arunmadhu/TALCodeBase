using System;
using System.ComponentModel.DataAnnotations;

namespace PremiumFinder.ApiServices.Models.Validations
{
    /// <summary>
    ///Custom Validator for checking date of birth
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class DOBValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime _dobInput = Convert.ToDateTime(value);

                if (_dobInput.Date > DateTime.Now.Date)
                {
                    return new ValidationResult("Date of birth cannot be greater than today");
                }
            }
            return ValidationResult.Success;

        }

    }
}
