using Enterprise.Constants.NetStandard;
using Enterprise.Enums.NetStandard;
using Enterprise.Extension.NetStandard;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Enterprise.AuthorizationServer
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource()
                {
                    Name=ApiNames.loggingserver,
                    DisplayName="Logging Server",
                    Description="Logging Server Access",
                    // Only Super Admin Can Access this...
                    UserClaims=new List<string>{"role"},
                    ApiSecrets={
                        new Secret(APISecrets.loggingserver.Sha256())
                    },
                    // Scopes Here is Application Based...
                    // Only Logging Client Has All Controls...
                    // Other Apps Only Has Write Access...
                    Scopes = new List<Scope>
                    {
                        new Scope()
                        {
                            Name=LoggingServerScopes.full_access,
                            DisplayName="Full access to Logging Server",
                        },
                        new Scope()
                        {
                            Name=LoggingServerScopes.read_only_access,
                            DisplayName="Read access to Logging Server",
                        },
                        new Scope()
                        {
                            Name=LoggingServerScopes.write_access,
                            DisplayName="Write access to Logging Server",
                        },
                        new Scope()
                        {
                            Name=LoggingServerScopes.delete_access,
                            DisplayName="Delete access to Logging Server",
                        }
                    },
                    Enabled=true
                },
                new ApiResource()
                {
                    Name=ApiNames.configurationserver,
                    DisplayName="Configuration Server",
                    Description="Configuration Server Access",
                    // Need Pass Roles for Scope Access
                    // Need Name For
                    UserClaims=new List<string>{
                        JwtClaimTypes.Role,
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Email
                    },
                    ApiSecrets={
                        new Secret(APISecrets.configurationserver.Sha256())
                    },
                    Scopes = new List<Scope>
                    {
                        new Scope()
                        {
                            Name=ConfigurationServerScopes.full_access,
                            DisplayName="Full access to Configuration Server",
                        },
                        new Scope()
                        {
                            Name=ConfigurationServerScopes.read_only_access,
                            DisplayName="Read access to Configuration Server",
                        },
                        new Scope()
                        {
                            Name=ConfigurationServerScopes.write_access,
                            DisplayName="Write access to Configuration Server",
                        }
                    },
                    Enabled=true
                },
            };
        }

        /// <summary>
        /// Specify OpenId Connect Scopes.
        /// Setting here for Display this in Consent Page, Required Attr, Enabled, etc...
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
                {
                    Required=true
                },
                new IdentityResources.Email()
                {
                    Required=true
                },
                new IdentityResources.Phone()
                {
                    Required=true
                },
                new IdentityResources.Address()
                {
                    Required=true
                },
                new IdentityResource()
                {
                    Name=JwtClaimTypes.Role,
                    DisplayName = "Role(s)",
                    Description = "roles of user",
                    Enabled=true,
                    Required=true,
                    UserClaims=new List<string>{ JwtClaimTypes.Role }
                },

            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = ClientIDs.configuration_webclient,
                    ClientName = "Enterprise Configuration Web Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = new List<string> { "openid", "profile", "LoggingServer.write_access" },
                    RedirectUris = new List<string> { "http://localhost:4200/auth-callback" },
                    PostLogoutRedirectUris = new List<string> { "http://localhost:4200/" },
                    AllowedCorsOrigins = new List<string> { "http://localhost:4200" },
                    AllowAccessTokensViaBrowser = true
                },
                new Client
                {
                    ClientId = ClientIDs.template_webclient,
                    ClientName = "Enterprise Template Web Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent=false,
                    AllowedScopes = new List<string> {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        JwtClaimTypes.Role
                    },

                    // Pop up Auth...
                    //RedirectUris = new List<string> { "http://localhost:4200/pop-up" },

                    // Silent Auth...
                    //RedirectUris = new List<string> { "http://localhost:4200/silent" },

                    // Redirect Auth ...
                    RedirectUris = new List<string> { "http://localhost:4200/auth-callback" },
                    PostLogoutRedirectUris = new List<string> { "http://localhost:4200/" },

                    AllowedCorsOrigins = new List<string> { "http://localhost:4200" }
                },
                new Client
                {
                    ClientId =ClientIDs.loggingserver_testclient,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret(ClientSecrets.loggingserver_testclient.Sha256())
                    },
                    AllowedScopes = { LoggingServerScopes.full_access }
                },
                new Client
                {
                    ClientId =ClientIDs.configurationserver_testclient,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret(ClientSecrets.configurationserver_testclient.Sha256())
                    },
                    AllowedScopes = { ConfigurationServerScopes.full_access }
                }
            };
        }
    }
}
