using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enterprise.AuthorizationServer.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Enterprise.AuthorizationServer.Pages.Account
{
    public class LoggedOutModel : PageModel
    {
        public LoggedOutViewModel LoggedOutViewModel { get; private set; }
        public void OnGet(LoggedOutViewModel loggedOutViewModel)
        {
            LoggedOutViewModel = loggedOutViewModel;
        }
    }
}