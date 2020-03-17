using System.ComponentModel.DataAnnotations;

namespace DotNetflix.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Enter a valid email adress")]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password incorrect")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
