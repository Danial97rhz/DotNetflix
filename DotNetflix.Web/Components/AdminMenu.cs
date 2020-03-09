using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.Web.Components
{
    public class AdminMenu : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var menuItems = new List<AdminMenuItem> { new AdminMenuItem()
                {
                    DisplayValue = "User management",
                    ActionValue = "UserManagement"

                },
                new AdminMenuItem()
                {
                    DisplayValue = "Role management",
                    ActionValue = "RoleManagement"
                }};

            return View(menuItems);
        }
    }
}
