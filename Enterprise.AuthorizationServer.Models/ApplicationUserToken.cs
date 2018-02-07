using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enterprise.AuthorizationServer.Models
{
    public class ApplicationUserToken:IdentityUserToken<Guid>
    {
    }
}
