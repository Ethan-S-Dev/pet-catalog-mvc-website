using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.MVC.Validations
{
    public class CategoryIdAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var val = value as int?;
            if (val is null) return new ValidationResult(ErrorMessage);
            if (val is 0) return new ValidationResult(ErrorMessage);
            if(val < -1) return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
