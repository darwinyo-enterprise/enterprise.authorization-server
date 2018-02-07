using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Enterprise.AuthorizationServer.Models;
using Enterprise.Models.NetStandard;
using Enterprise.Enums.NetStandard;
using Enterprise.Extension.NetStandard;
using Enterprise.Constants.NetStandard;
using Enterprise.Interfaces.NetStandard.Services;

namespace Enterprise.AuthorizationServer.Pages.Account.Manage
{
    public class ResetAuthenticatorModel : PageModel
    {
        UserManager<ApplicationUser> _userManager;

        #region Template
        private readonly ILoggingServices _loggingServices;
        private readonly LogModel logModel;
        private readonly UserScopesModel userScopesModel;
        #endregion

        public ResetAuthenticatorModel(
            UserManager<ApplicationUser> userManager,
            ILoggingServices loggingServices)
        {
            _userManager = userManager;

            #region Template
            _loggingServices = loggingServices;

            logModel = new LogModel();
            userScopesModel = new UserScopesModel(HttpContext);
            logModel.SetModel(userScopesModel.Subject.ToString(), userScopesModel.Name, nameof(ResetAuthenticatorModel), ApplicationNames.AuthorizationServer, LogTypeEnum.Info);
            #endregion
        }
        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await _userManager.SetTwoFactorEnabledAsync(user, false);
            await _userManager.ResetAuthenticatorKeyAsync(user);
            logModel.LogMessage = string.Format("User with ID '{0}' has reset their authentication app key.", user.Id);

            // Logging
            await _loggingServices.LogAsync(logModel);

            return RedirectToPage("./EnableAuthenticator");
        }
    }
}