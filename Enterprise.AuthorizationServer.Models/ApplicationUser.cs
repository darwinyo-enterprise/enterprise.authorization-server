﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.AuthorizationServer.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
    }
}
