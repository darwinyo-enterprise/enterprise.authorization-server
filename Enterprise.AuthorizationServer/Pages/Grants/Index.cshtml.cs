using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enterprise.AuthorizationServer.ViewModels.GrantViewModels;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Enterprise.AuthorizationServer.Pages.Grants
{
    public class IndexModel : PageModel
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clients;
        private readonly IResourceStore _resources;
        
        public GrantsViewModel GrantsViewModel { get; set; }
        public IndexModel(IIdentityServerInteractionService interaction,
            IClientStore clients,
            IResourceStore resources)
        {
            _interaction = interaction;
            _clients = clients;
            _resources = resources;

        }
        public async Task<IActionResult> OnGetAsync()
        {
            await BuildViewModelAsync();
            return Page();
        }
        public async Task<IActionResult> OnPostRevokeAsync(string clientId)
        {
            await _interaction.RevokeUserConsentAsync(clientId);
            return Page();
        }
        private async Task<GrantsViewModel> BuildViewModelAsync()
        {
            var grants = await _interaction.GetAllUserConsentsAsync();

            var list = new List<GrantViewModel>();
            foreach (var grant in grants)
            {
                var client = await _clients.FindClientByIdAsync(grant.ClientId);
                if (client != null)
                {
                    var resources = await _resources.FindResourcesByScopeAsync(grant.Scopes);

                    var item = new GrantViewModel()
                    {
                        ClientId = client.ClientId,
                        ClientName = client.ClientName ?? client.ClientId,
                        ClientLogoUrl = client.LogoUri,
                        ClientUrl = client.ClientUri,
                        Created = grant.CreationTime,
                        Expires = grant.Expiration,
                        IdentityGrantNames = resources.IdentityResources.Select(x => x.DisplayName ?? x.Name).ToArray(),
                        ApiGrantNames = resources.ApiResources.Select(x => x.DisplayName ?? x.Name).ToArray()
                    };

                    list.Add(item);
                }
            }
            GrantsViewModel= new GrantsViewModel
            {
                Grants = list
            };
            return GrantsViewModel;
        }
    }
}