using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MiniBankingProject.Domain.Features.Validation
{
    public class NumericOnlyAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult($"{validationContext.DisplayName} is required.");
            }

            string? strValue = value.ToString();

            if (strValue == null || !strValue.All(char.IsDigit))
            {
                return new ValidationResult($"{validationContext.DisplayName} must contain only numbers.");
            }

            return ValidationResult.Success!;
        }
    }

}
