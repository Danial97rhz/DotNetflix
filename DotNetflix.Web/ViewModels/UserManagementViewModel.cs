using DotNetflix.Web.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.Web.ViewModels
{
    public class UserManagementViewModel
    {
        public IEnumerable<UserManagementUser> Users { get; set; }
    }

    public class UserManagementUser
    {
        public ApplicationUser User { get; set; }
        public ApplicationRole Role { get; set; }
    }
}
