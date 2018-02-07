using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Enterprise.AuthorizationServer.Models;
using Enterprise.Models.NetStandard;
using Enterprise.Extension.NetStandard;
using Enterprise.Enums.NetStandard;
using IdentityModel;
using Enterprise.Constants.NetStandard;
using Enterprise.Interfaces.NetStandard.Services;

namespace Enterprise.AuthorizationServer.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        #region Template
        private readonly ILoggingServices _loggingServices;
        private readonly LogModel logModel;
        private readonly UserScopesModel userScopesModel;
        #endregion

        public AccountController(SignInManager<ApplicationUser> signInManager, ILoggingServices loggingServices)
        {
            _signInManager = signInManager;

            #region Template
            _loggingServices = loggingServices;
            logModel = new LogModel();
            userScopesModel = new UserScopesModel(HttpContext);
            logModel.SetModel(userScopesModel.Subject.ToString(), userScopesModel.Name, nameof(AccountController), ApplicationNames.AuthorizationServer, LogTypeEnum.Info);
            #endregion
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            // Set Log Message
            logModel.LogMessage = "User logged out.";

            // Audit Log
            await _loggingServices.LogAsync(logModel);

            return RedirectToPage("/Index");
        }
    }
}
