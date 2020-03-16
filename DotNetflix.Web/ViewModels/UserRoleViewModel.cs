using DotNetflix.Web.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.Web.ViewModels
{
    public class UserRoleViewModel
    {
        public UserRoleViewModel()
        {
            Users = new List<ApplicationUser>();
        }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public List<ApplicationUser> Users { get; set; }
    }
}
