using DotNetflix.Web.Auth;

namespace DotNetflix.Web.ViewModels
{
    public class LoginComponentViewModel
    {
        public ApplicationUser User { get; set; }
        public bool IsSignedIn { get; set; }
    }
    
}
