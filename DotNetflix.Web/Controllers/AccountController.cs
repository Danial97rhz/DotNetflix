using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DotNetflix.Web.Auth;
using DotNetflix.Web.ViewModels;
using System;
using EmailService;
using System.Linq;
using DotNetflix.Web.Models;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;

namespace DotNetflix.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        /***************************************************
                        REGISTER NEW USER
        ***************************************************/
        [HttpGet] // SHOW VIEW TO REGISTER NEW USER
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
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
                    Birthdate = registerUserViewModel.Birthdate != null ?
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

                    // If registration confirmation is required send confirmation email else login user.
                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Action(
                            "ConfirmEmail",
                            "Account",
                            new { code, userId = user.Id },
                            Request.Scheme);

                        var message = new Message(
                            registerUserViewModel.Email,
                            "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        _emailSender.SendEmail(message);

                        return RedirectToAction("RegisterConfirmation", "Account");
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "Home");
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(registerUserViewModel);
        }

        [HttpGet] // SHOW EMAIL SENT CONFIRMATION MESSAGE
        public IActionResult RegisterConfirmation()
        {
            return View();
        }

        [HttpGet] // CONFIRM EMAIL 
        public async Task<IActionResult> ConfirmEmail(string code, string userId)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            var vm = new ConfirmEmailViewModel
            {
                StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email."
            };

            return View(vm);
        }


        /**********************************************
                        LOGIN / LOGOUT
        ***********************************************/
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

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }


        /************************************************
                     MY ACCOUNT / EDIT USER
        ************************************************/
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
            vm.Birthdate = user.Birthdate != null ? user.Birthdate : new System.DateTime();
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
                user.Birthdate = editUserViewModel.Birthdate;
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

        [HttpPost]
        public async Task<IActionResult> UploadAvatar(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);

                // Upload the file if less than 2 MB
                if (ms.Length < 2097152)
                {
                    // Call edit user to update user with new avatar
                    var user = await _userManager.GetUserAsync(User);
                    user.Avatar = ms.ToArray();

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("MyAccount");


                }
            }               
            return RedirectToAction("MyAccount");
        }

        [HttpGet]
        public async Task<IActionResult> GetAvatar()
        {
            var user = await _userManager.GetUserAsync(User);

            FileResult avatar = File(user.Avatar, "image/png");

            return avatar;
        }


        /***************************************************
                      RESET FORGOTTEN PASSWORD
        ***************************************************/
        [HttpGet] // LET THE USER ENTER A EMAIL ADDRESS TO SEND CONFIRMATION LINK TO
        public IActionResult ForgotPassword()
        {
            return View();
        }


        [HttpPost] // SEND CONFIRMATION LINK TO USER EMAIL
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(vm.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToAction("ForgotPasswordConfirmation", "Account");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Action(
                    "ResetPassword",
                    "Account",
                    new { code, email = user.Email },
                    Request.Scheme);

                var message = new Message(
                    vm.Email,
                    "Reset Password",
                    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                _emailSender.SendEmail(message);

                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }
            return View(vm);
        }

        [HttpGet] // SHOW EMAIL SENT CONFIRMATION MESSAGE
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet] // SHOW VIEW FOR USER TO ENTER NEW PASSWORD
        public IActionResult ResetPassword(string code, string email)
        {
            if (code == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }
            else
            {
                var vm = new ResetPasswordViewModel
                {
                    Code = code,
                    Email = email
                };
                return View(vm);
            }
        }

        [HttpPost] // RESET THE USERS PASSWORD
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.FindByEmailAsync(vm.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }

            var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(vm.Code));
            var result = await _userManager.ResetPasswordAsync(user, code, vm.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(vm);
        }

        [HttpGet] // SHOW RESET PASSWORD CONFIRMATION MESSAGE
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}
