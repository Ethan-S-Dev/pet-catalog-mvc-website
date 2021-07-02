using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PetShop.MVC.Validations;
using PetShop.MVC.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.MVC.Models
{
    public class AnimalFormModel
    {
        private readonly IConfiguration configuration;
        public AnimalFormModel(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public int AnimalId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        [Required(ErrorMessage ="Please select an image.")]
        [DataType(DataType.Upload)]
        [AllowedExtensions(".png",".jpg",".svg",".jpeg",".webp",ErrorMessage ="Invalid image type.")]
        [MaxFileSize(65536,ErrorMessage ="Image must be smaller then 64 KB.")]
        public IFormFile MyProperty { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
}
