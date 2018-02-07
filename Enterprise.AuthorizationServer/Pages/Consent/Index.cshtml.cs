using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enterprise.AuthorizationServer.Models.Consent;
using Enterprise.AuthorizationServer.Services;
using Enterprise.AuthorizationServer.ViewModels.ConsentViewModels;
using Enterprise.FilterAttributes.NetStandard;
using Enterprise.Interfaces.NetStandard.Services;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Enterprise.AuthorizationServer.Pages.Consent
{
    [SecurityHeaders]
    public class IndexModel : PageModel
    {
        private readonly ConsentService<IndexModel> _consent;
        public ConsentViewModel ConsentViewModel { get; set; }
        public IndexModel(IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IResourceStore resourceStore,
            ILoggingServices loggingServices)
        {
            _consent = new ConsentService<IndexModel>(interaction, clientStore, resourceStore,HttpContext, loggingServices);
        }
        public async Task<IActionResult> OnGetAsync(string returnUrl)
        {
            ConsentViewModel = await _consent.BuildViewModelAsync(returnUrl);
            if (ConsentViewModel != null)
            {
                return await OnPostAsync(ConsentViewModel);
            }

            return RedirectToPage("../Error");
        }
        public async Task<IActionResult> OnPostAsync(ConsentInputModel model)
        {
            var result = await _consent.ProcessConsent(model);

            if (result.IsRedirect)
            {
                return Redirect(result.RedirectUri);
            }

            if (result.HasValidationError)
            {
                ModelState.AddModelError("", result.ValidationError);
            }

            if (result.ShowView)
            {
                ConsentViewModel = result.ViewModel;
                return Page();
            }

            return RedirectToPage("../Error");
        }
    }
}