using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServices
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() => new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };

        public static IEnumerable<ApiResource> GetApis() => new List<ApiResource>
            {
                new ApiResource("api.services", "Apollos Api Services")
            };

        public static IEnumerable<Client> GetClients() => new List<Client>
            {                
                // website: SPA client using implicit flow
                new Client
                {
                   ClientId = "website",
                   ClientName = "Atlas Website Homepage",
                   ClientUri = "http://localhost:7002",
                   AllowedGrantTypes = GrantTypes.Implicit,
                   AllowAccessTokensViaBrowser = true,
                   RedirectUris = {
                      "http://localhost:7002/index.html",
                      "http://localhost:7002/callback.html",
                      "http://localhost:7002/silent.html",
                      "http://localhost:7002/popup.html"
                   },
                   PostLogoutRedirectUris = {
                        "http://localhost:7002/index.html"
                   },
                   AllowedCorsOrigins = {
                        "http://localhost:7002"
                   },
                   AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "api.services"
                   }
                },
                // MVC client: OpenID Connect hybrid flow client (MVC)
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RequireConsent = true,
                    AllowRememberConsent = true,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris           = { "http://localhost:7006/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:7006/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "api.services"
                    },

                    AllowOfflineAccess = true
                },
            };
    }
}