using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Enterprise.AuthorizationServer.Models;

namespace Enterprise.AuthorizationServer.DataLayers
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRoles, Guid, ApplicationUserClaims,
        ApplicationUserRoles, ApplicationUserLogins, ApplicationRoleClaim, ApplicationUserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable("ApplicationUser", "Authentication");

                entity.HasKey(e => e.Id);
            });
            builder.Entity<ApplicationRoles>(entity =>
            {
                entity.ToTable("ApplicationRoles", "Authentication");
            });
            builder.Entity<ApplicationUserClaims>(entity =>
            {
                entity.ToTable("ApplicationUserClaims", "Authentication");
            });
            builder.Entity<ApplicationUserRoles>(entity =>
            {
                entity.ToTable("ApplicationUserRoles", "Authentication");
            });
            builder.Entity<ApplicationUserLogins>(entity =>
            {
                entity.ToTable("ApplicationUserLogins", "Authentication");
            });
            builder.Entity<ApplicationRoleClaim>(entity =>
            {
                entity.ToTable("ApplicationRoleClaim", "Authentication");
            });
            builder.Entity<ApplicationUserToken>(entity =>
            {
                entity.ToTable("ApplicationUserToken", "Authentication");
            });
        }
    }
}
