using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Enterprise.AuthorizationServer.Models;
using Enterprise.Enums.NetStandard;
using Enterprise.Models.NetStandard;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Enterprise.Extension.NetStandard;
using Enterprise.Constants.NetStandard;
using Enterprise.Interfaces.NetStandard.Services;

namespace Enterprise.AuthorizationServer.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly SignInManager<ApplicationUser> _signInManager;

        #region Template
        private readonly ILoggingServices _loggingServices;

        private readonly LogModel logModel;
        private readonly UserScopesModel userScopesModel;
        #endregion

        public LoginModel(SignInManager<ApplicationUser> signInManager,
            IIdentityServerInteractionService interaction,
            ILoggingServices loggingServices)
        {
            _signInManager = signInManager;

            #region Template
            _loggingServices = loggingServices;

            logModel = new LogModel();
            userScopesModel = new UserScopesModel(HttpContext);
            logModel.SetModel(userScopesModel.Subject.ToString(), userScopesModel.Name, nameof(LoginModel), ApplicationNames.AuthorizationServer, LogTypeEnum.Info);
            #endregion

            _interaction = interaction;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;

            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            if (context?.IdP != null && ExternalLogins.Any(p => string.Equals(p.Name, context.IdP, StringComparison.InvariantCultureIgnoreCase)))
            {
                return RedirectToPage("./ExternalLogin", new { LoginProvider = context.IdP, ProviderKey = returnUrl });
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    logModel.LogMessage = "User logged in.";
                    
                    // Logging
                    await _loggingServices.LogAsync(logModel);

                    return LocalRedirect(Url.GetLocalUrl(returnUrl));
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    logModel.LogMessage = "User account locked out.";
                    logModel.LogType = LogTypeEnum.Warn;

                    // Logging
                    await _loggingServices.LogAsync(logModel);

                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
