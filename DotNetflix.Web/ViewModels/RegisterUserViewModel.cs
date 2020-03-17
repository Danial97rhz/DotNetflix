using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DotNetflix.Web.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "Enter a valid email adress")]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "Birth date (optional)")]
        //[DataType(DataType.Date)] Bortkommenterad för att det ej går att lämna fältet tomt vid en post annars
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthdate { get; set; }

        [Display(Name = "City (optional)")]
        public string City { get; set; }

        [Display(Name = "Country (optional)")]
        public string Country { get; set; }
    }
}
