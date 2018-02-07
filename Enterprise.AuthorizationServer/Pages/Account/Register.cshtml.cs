using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Enterprise.AuthorizationServer.Models;
using Enterprise.AuthorizationServer.Services;
using Enterprise.Services.Interfaces;
using Enterprise.Models.NetStandard;
using Enterprise.Enums.NetStandard;
using Enterprise.Extension.NetStandard;
using System.Security.Claims;
using IdentityModel;
using Enterprise.Constants.NetStandard;
using Enterprise.Interfaces.NetStandard.Services;

namespace Enterprise.AuthorizationServer.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailServices _emailSender;

        #region Template
        private readonly ILoggingServices _loggingServices;
        private readonly LogModel logModel;
        private readonly UserScopesModel userScopesModel;
        #endregion

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailServices emailSender,
            ILoggingServices loggingServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;

            #region Template
            _loggingServices = loggingServices;

            logModel = new LogModel();
            userScopesModel = new UserScopesModel(HttpContext);
            logModel.SetModel(userScopesModel.Subject.ToString(), userScopesModel.Name, nameof(RegisterModel), ApplicationNames.AuthorizationServer, LogTypeEnum.Info);
            #endregion
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(25, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [Display(Name = "UserName")]
            public string UserName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
            [DataType(DataType.PhoneNumber)]
            [Display(Name = "PhoneNumber")]
            public string PhoneNumber { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.UserName, Email = Input.Email, PhoneNumber = Input.PhoneNumber };
                IEnumerable<Claim> claims = new List<Claim>
                {
                    new Claim(JwtClaimTypes.Name,Input.UserName),
                    new Claim(JwtClaimTypes.Email,Input.Email),
                    new Claim(JwtClaimTypes.PhoneNumber,Input.PhoneNumber),
                    new Claim(JwtClaimTypes.Role,AppRoleNames.ECommerce_End_User)
                };

                var result = await _userManager.CreateAsync(user, Input.Password);

                // Add User To Roles
                result = await _userManager.AddToRoleAsync(user, AppRoleNames.ECommerce_End_User);

                // Add Claim To User
                result = await _userManager.AddClaimsAsync(user, claims);

                if (result.Succeeded)
                {
                    logModel.LogMessage = "User created a new account with password.";

                    // Logging
                    await _loggingServices.LogAsync(logModel);

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.EmailConfirmationLink(user.Id.ToString(), code, Request.Scheme);
                    await _emailSender.SendEmailConfirmationAsync(Input.Email, callbackUrl);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(Url.GetLocalUrl(returnUrl));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
