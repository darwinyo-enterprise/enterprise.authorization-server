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
    public class Disable2faModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        #region Template
        private readonly ILoggingServices _loggingServices;
        private readonly LogModel logModel;
        private readonly UserScopesModel userScopesModel;
        #endregion

        public Disable2faModel(
            UserManager<ApplicationUser> userManager,
            ILoggingServices loggingServices)
        {
            _userManager = userManager;

            #region Template
            _loggingServices = loggingServices;
            logModel = new LogModel();
            userScopesModel = new UserScopesModel(HttpContext);
            logModel.SetModel(userScopesModel.Subject.ToString(), userScopesModel.Name, nameof(Disable2faModel), ApplicationNames.AuthorizationServer, LogTypeEnum.Info);
            #endregion
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!await _userManager.GetTwoFactorEnabledAsync(user))
            {
                throw new ApplicationException($"Cannot disable 2FA for user with ID '{_userManager.GetUserId(User)}' as it's not currently enabled.");
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

            var disable2faResult = await _userManager.SetTwoFactorEnabledAsync(user, false);
            if (!disable2faResult.Succeeded)
            {
                throw new ApplicationException($"Unexpected error occurred disabling 2FA for user with ID '{_userManager.GetUserId(User)}'.");
            }
            logModel.LogMessage = string.Format("User with ID '{0}' has disabled 2fa.", _userManager.GetUserId(User));

            // Logging
            await _loggingServices.LogAsync(logModel);

            return RedirectToPage("./TwoFactorAuthentication");
        }
    }
}