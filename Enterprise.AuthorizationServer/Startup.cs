using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Enterprise.AuthorizationServer.DataLayers;
using Enterprise.AuthorizationServer.Models;
using Enterprise.Services.Interfaces;
using Enterprise.Services;
using System.Reflection;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Enterprise.Constants.NetStandard;
using Enterprise.Interfaces.NetStandard.Services;
using Enterprise.Helpers.NetStandard;

namespace Enterprise.AuthorizationServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            // Required in all applications
            InitializeStartupHelper.InitializeStaticFields(Configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString(ConfigurationNames.AuthorizationConnection);

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Enterprise.AuthorizationServer"));
            });

            services.AddIdentity<ApplicationUser, ApplicationRoles>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/Account/Manage");
                    options.Conventions.AuthorizePage("/Account/Logout");
                });

            // configure identity server with in-memory stores, keys, clients and scopes
            //services.AddIdentityServer()
            //    .AddDeveloperSigningCredential()
            //    .AddAspNetIdentity<ApplicationUser>()
            //    .AddConfigurationStore(options =>
            //    {
            //        options.ConfigureDbContext = builder =>
            //            builder.UseSqlServer(connectionString,
            //                sql => sql.MigrationsAssembly(migrationsAssembly));

            //        options.DefaultSchema = "Authorization";
            //    })
            //    // this adds the operational data from DB (codes, tokens, consents)
            //    .AddOperationalStore(options =>
            //    {
            //        options.ConfigureDbContext = builder =>
            //            builder.UseSqlServer(connectionString,
            //                sql => sql.MigrationsAssembly(migrationsAssembly));

            //        // this enables automatic token cleanup. this is optional.
            //        options.EnableTokenCleanup = true;
            //        options.TokenCleanupInterval = 30;

            //        options.DefaultSchema = "Authorization";
            //    });
            services.AddIdentityServer()
               .AddDeveloperSigningCredential()
               .AddInMemoryIdentityResources(Config.GetIdentityResources())
               .AddInMemoryApiResources(Config.GetApiResources())
               .AddInMemoryClients(Config.GetClients())
                   //.AddTestUsers(Config.GetUsers());
                   .AddAspNetIdentity<ApplicationUser>();

            // Register no-op EmailSender used by account confirmation and password reset during development
            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
            services.AddSingleton<IEmailServices, EmailServices>();


            #region StartUp templates
            // this below needs static URL application values to operate.
            services.AddScoped<ILoggingServices, LoggingServices>();
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //PopulateIdentityServer(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            // app.UseAuthentication(); // not needed, since UseIdentityServer adds the authentication middleware
            app.UseIdentityServer();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }

        // Populate DB
        private void PopulateIdentityServer(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();
                if (!context.Clients.Any())
                {
                    foreach (var client in Config.GetClients())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.GetIdentityResources())
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Config.GetApiResources())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }

        }
    }
}
