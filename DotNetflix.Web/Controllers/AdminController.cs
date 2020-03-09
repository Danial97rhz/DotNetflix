using DotNetflix.Web.Auth;
using DotNetflix.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DotNetflix.Web.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserManagement()
        {
            var users = _userManager.Users;

            return View(users);
        }

        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserViewModel addUserViewModel)
        {
            if (!ModelState.IsValid) return View(addUserViewModel);

            var user = new ApplicationUser()
            {
                UserName = addUserViewModel.UserName,
                Email = addUserViewModel.Email,
                BirthDate = addUserViewModel.Birthdate,
                City = addUserViewModel.City,
                Country = addUserViewModel.Country
            };

            IdentityResult result = await _userManager.CreateAsync(user, addUserViewModel.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("UserManagement", _userManager.Users);
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(addUserViewModel);
        }

        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return RedirectToAction("UserManagement", _userManager.Users);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(string id, string UserName, string Email)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                user.Email = Email;
                user.UserName = UserName;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                    return RedirectToAction("UserManagement", _userManager.Users);

                ModelState.AddModelError("", "User not updated, something went wrong.");

                return View(user);
            }

            return RedirectToAction("UserManagement", _userManager.Users);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("UserManagement");
                else
                    ModelState.AddModelError("", "Something went wrong while deleting this user.");
            }
            else
            {
                ModelState.AddModelError("", "This user can't be found");
            }
            return View("UserManagement", _userManager.Users);
        }

        // Roll management
        public IActionResult RoleManagement()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        public IActionResult AddNewRole() => View();

        [HttpPost]
        public async Task<IActionResult> AddNewRole(AddRoleViewModel addRoleViewModel)
        {

            if (!ModelState.IsValid) return View(addRoleViewModel);

            var role = new IdentityRole
            {
                Name = addRoleViewModel.RoleName
            };

            IdentityResult result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("RoleManagement", _roleManager.Roles);
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(addRoleViewModel);
        }
    }
}
