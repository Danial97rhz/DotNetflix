using System.ComponentModel.DataAnnotations;

namespace DotNetflix.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "User name incorrect")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password incorrect")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
