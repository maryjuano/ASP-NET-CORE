using System;
using System.ComponentModel.DataAnnotations;

namespace GSC.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}