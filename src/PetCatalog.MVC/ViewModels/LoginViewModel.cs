using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.MVC.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public string RedirectPath { get; set; }

        [Display(Name = "Keep me logged in")]
        public bool KeepLoggedIn { get; set; }
    }
}
