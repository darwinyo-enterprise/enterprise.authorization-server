using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enterprise.AuthorizationServer.Models.Account;
using Enterprise.AuthorizationServer.Services;
using Enterprise.AuthorizationServer.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Enterprise.AuthorizationServer.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public LogoutViewModel logoutViewModel;
        private readonly AccountService _account;
        public LogoutModel(AccountService account)
        {
            _account = account;
        }
        public async Task<IActionResult> OnGet(string logoutId)
        {
            // build a model so the logout page knows what to display
            var vm = await _account.BuildLogoutViewModelAsync(logoutId);

            if (vm.ShowLogoutPrompt == false)
            {
                // if the request for logout was properly authenticated from IdentityServer, then
                // we don't need to show the prompt and can just log the user out directly.
                return await OnPost(vm);
            }
            return Page();
        }
        public async Task<IActionResult> OnPost(LogoutInputModel model)
        {
            // build a model so the logged out page knows what to display
            var vm = await _account.BuildLoggedOutViewModelAsync(model.LogoutId);

            var user = HttpContext.User;
            if (user?.Identity.IsAuthenticated == true)
            {
                // delete local authentication cookie
                await HttpContext.SignOutAsync();

                // raise the logout event
                //await _events.RaiseAsync(new UserLogoutSuccessEvent(user.GetSubjectId(), user.GetDisplayName()));
            }

            // check if we need to trigger sign-out at an upstream identity provider
            if (vm.TriggerExternalSignout)
            {
                // build a return URL so the upstream provider will redirect back
                // to us after the user has logged out. this allows us to then
                // complete our single sign-out processing.
                string url = Url.Action("Logout", new { logoutId = vm.LogoutId });

                // this triggers a redirect to the external provider for sign-out
                return SignOut(new AuthenticationProperties { RedirectUri = url }, vm.ExternalAuthenticationScheme);
            }

            return RedirectToPage("./LoggedOut", vm);
        }
    }
}