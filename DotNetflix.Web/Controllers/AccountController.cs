using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DotNetflix.Web.Auth;
using DotNetflix.Web.ViewModels;
using System;

namespace DotNetflix.Web.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return View(loginViewModel);

            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginViewModel.ReturnUrl))
                        return RedirectToAction("Index", "Home");

                    return Redirect(loginViewModel.ReturnUrl);
                }
            }

            ModelState.AddModelError("", "Username/password not found");
            return View(loginViewModel);
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel registerUserViewModel)
        {
            if (ModelState.IsValid)
            {
                /* Create new user with the values given at registration then add user to db */
                var user = new ApplicationUser() 
                {
                    Email = registerUserViewModel.Email,
                    UserName = registerUserViewModel.UserName != null ?
                        registerUserViewModel.UserName : registerUserViewModel.Email,
                    BirthDate = registerUserViewModel.Birthdate != null ?
                        registerUserViewModel.Birthdate : new DateTime(),
                    City = registerUserViewModel.City != null ?
                        registerUserViewModel.City : "Unknown",
                    Country = registerUserViewModel.Country != null ?
                        registerUserViewModel.Country : "Unknown"                   
                };

                var result = await _userManager.CreateAsync(user, registerUserViewModel.Password);

                if (result.Succeeded)
                {
                    /* When a new user is added to the db give it the role of "User" */
                    var role = new ApplicationRole() { Name = "User" };
                    var resultRole = await _userManager.AddToRoleAsync(user, role.Name);
                    if (resultRole.Succeeded)
                    {
                        var vm = new LoginViewModel
                        {
                            Email = user.Email,
                            Password = registerUserViewModel.Password
                        };
                        return await Login(vm);
                    }                        
                    else
                        TempData["PostError"] = "Could not add new user to role, try again or contact support!";
                }
                else
                    TempData["PostError"] = "Could not create new user, try again or contact support!";
            }
            return View(registerUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public async Task<IActionResult> MyAccount()
        {
            var vm = new MyAccountViewModel()
            {
                User = await _userManager.GetUserAsync(HttpContext.User)
            };

            return View(vm);
        }


        public async Task<IActionResult> EditUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
                return RedirectToAction("MyAccount");

            // Create new EditUserView and fill with data from user object retrived from db
            var vm = new EditUserViewModel();
            //vm.Id = user.Id != null ? user.Id : "Unkown";
            vm.Email = user.Email != null ? user.Email : "Unkown";
            vm.UserName = user.UserName != null ? user.UserName : "Unkown";
            vm.Birthdate = user.BirthDate != null ? user.BirthDate : new System.DateTime();
            vm.City = user.City != null ? user.City : "Unkown";
            vm.Country = user.Country != null ? user.Country : "Unkown";

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel editUserViewModel)
        {
            var user = await _userManager.FindByIdAsync(editUserViewModel.Id.ToString());

            if (user != null)
            {
                user.Email = editUserViewModel.Email;
                user.UserName = editUserViewModel.UserName;
                user.BirthDate = editUserViewModel.Birthdate;
                user.City = editUserViewModel.City;
                user.Country = editUserViewModel.Country;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                    return RedirectToAction("MyAccount");

                ModelState.AddModelError("", "User not updated, something went wrong.");

                return View(editUserViewModel);
            }

            return RedirectToAction("MyAccount");
        }

        //[AllowAnonymous]
        //public IActionResult GoogleLogin(string returnUrl = null)
        //{
        //    var redirectUrl = Url.Action("GoogleLoginCallback", "Account", new { ReturnUrl = returnUrl });
        //    var properties = _signInManager.ConfigureExternalAuthenticationProperties(ExternalLoginServiceConstants.GoogleProvider, redirectUrl);
        //    return Challenge(properties, ExternalLoginServiceConstants.GoogleProvider);
        //}

        //[AllowAnonymous]
        //public async Task<IActionResult> GoogleLoginCallback(string returnUrl = null, string serviceError = null)
        //{
        //    if (serviceError != null)
        //    {
        //        ModelState.AddModelError(string.Empty, $"Error from external provider: {serviceError}");
        //        return View(nameof(Login));
        //    }

        //    var info = await _signInManager.GetExternalLoginInfoAsync();
        //    if (info == null)
        //    {
        //        return RedirectToAction(nameof(Login));
        //    }

        //    var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
        //    if (result.Succeeded)
        //    {
        //        if (returnUrl == null)
        //            return RedirectToAction("index", "home");

        //        return Redirect(returnUrl);
        //    }

        //    var user = new ApplicationUser
        //    {
        //        Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
        //        UserName = info.Principal.FindFirst(ClaimTypes.Email).Value
        //    };

        //    var identityResult = await _userManager.CreateAsync(user);

        //    if (!identityResult.Succeeded) return AccessDenied();

        //    identityResult = await _userManager.AddLoginAsync(user, info);

        //    if (!identityResult.Succeeded) return AccessDenied();

        //    await _signInManager.SignInAsync(user, false);

        //    if (returnUrl == null)
        //        return RedirectToAction("index", "home");

        //    return Redirect(returnUrl);
        //}
    }
}
