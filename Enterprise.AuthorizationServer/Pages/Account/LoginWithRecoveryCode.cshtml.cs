using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Enterprise.AuthorizationServer.Models;
using Enterprise.Enums.NetStandard;
using Enterprise.Models.NetStandard;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Enterprise.Extension.NetStandard;
using Enterprise.Constants.NetStandard;
using Enterprise.Interfaces.NetStandard.Services;

namespace Enterprise.AuthorizationServer.Pages.Account
{
    public class LoginWithRecoveryCodeModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        #region Template
        private readonly ILoggingServices _loggingServices;
        private readonly LogModel logModel;
        private readonly UserScopesModel userScopesModel;
        #endregion

        public LoginWithRecoveryCodeModel(SignInManager<ApplicationUser> signInManager,ILoggingServices loggingServices)
        {
            _signInManager = signInManager;

            #region Template
            _loggingServices = loggingServices;

            logModel = new LogModel();
            userScopesModel = new UserScopesModel(HttpContext);
            logModel.SetModel(userScopesModel.Subject.ToString(), userScopesModel.Name, nameof(LoginWithRecoveryCodeModel), ApplicationNames.AuthorizationServer, LogTypeEnum.Info);
            #endregion
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [BindProperty]
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Recovery Code")]
            public string RecoveryCode { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            ReturnUrl = returnUrl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            var recoveryCode = Input.RecoveryCode.Replace(" ", string.Empty);

            var result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);
                
            if (result.Succeeded)
            {
                logModel.LogMessage = string.Format("User with ID '{0}' logged in with a recovery code.", user.Id);
                
                // Logging
                await _loggingServices.LogAsync(logModel);
                return LocalRedirect(Url.GetLocalUrl(returnUrl));
            }
            if (result.IsLockedOut)
            {
                logModel.LogMessage = string.Format("User with ID '{0}' account locked out.", user.Id);
                logModel.LogType = LogTypeEnum.Warn;

                // Logging
                await _loggingServices.LogAsync(logModel);
                return RedirectToPage("./Lockout");
            }
            else
            {
                logModel.LogMessage = string.Format("Invalid recovery code entered for user with ID '{0}' ", user.Id);
                logModel.LogType = LogTypeEnum.Warn;

                // Logging
                await _loggingServices.LogAsync(logModel);
                ModelState.AddModelError(string.Empty, "Invalid recovery code entered.");
                return Page();
            }
        }
    }
}
