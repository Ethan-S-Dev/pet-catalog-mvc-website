using PetShop.Application.Interfaces;
using PetShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.MVC.Validations
{
    public class CategoryNameAttribute : ValidationAttribute
    {
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var val = value as string;
            if (string.IsNullOrWhiteSpace(val)) return new ValidationResult(ErrorMessage);           

            return ValidationResult.Success;
        }
    }
}
