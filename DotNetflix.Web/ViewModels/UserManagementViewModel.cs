using DotNetflix.Web.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.Web.ViewModels
{
    public class UserManagementViewModel
    {
        public UserManagementViewModel()
        {
            Users = new List<UserManagementUser>();
        }

        public List<UserManagementUser> Users { get; set; }
    }

    public class UserManagementUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public IList<string> Role { get; set; }
    }
}
