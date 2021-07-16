using System.ComponentModel.DataAnnotations;

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
