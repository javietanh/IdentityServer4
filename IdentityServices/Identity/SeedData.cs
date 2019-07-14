using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServices.Data;
using IdentityServices.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Claims;

namespace IdentityServices
{
    public class SeedData
    {
        public static void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                // initialized identity server persisted grant db
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                // initialized aspnet identity db
                serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
                // initialized identity server configuration db
                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                // apply migrations.
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
                    foreach (var resource in Config.GetApis())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                #region initial super admin
                var userMgr = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var superAdmin = userMgr.FindByEmailAsync("super@apollos.edu").Result;
                if (superAdmin == null)
                {
                    superAdmin = new ApplicationUser
                    {
                        UserName = "super",
                        Email = "super@apollos.edu",
                        EmailConfirmed = true,
                    };
                    var result = userMgr.CreateAsync(superAdmin, "P@ssw0rd").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    superAdmin = userMgr.FindByEmailAsync("super@apollos.edu").Result;

                    result = userMgr.AddClaimsAsync(superAdmin, new Claim[]{
                                new Claim(JwtClaimTypes.Name, "Super Admin"),
                                new Claim(JwtClaimTypes.GivenName, "Super"),
                                new Claim(JwtClaimTypes.FamilyName, "Admin"),
                                new Claim(JwtClaimTypes.Email, "super@apollos.edu"),
                                new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                                new Claim(JwtClaimTypes.WebSite, "http://apollos.edu"),
                                new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                            }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    Console.WriteLine("super administrator created");
                }
                else
                {
                    Console.WriteLine("super administrator already exists");
                }
                #endregion
            }
        }

        public static void EnsureSeedData(IServiceProvider provider)
        {
            provider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
            provider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
            provider.GetRequiredService<ConfigurationDbContext>().Database.Migrate();

            {
                var userMgr = provider.GetRequiredService<UserManager<ApplicationUser>>();
                var superAdmin = userMgr.FindByEmailAsync("super@apollos.edu").Result;
                if (superAdmin == null)
                {
                    superAdmin = new ApplicationUser
                    {
                        UserName = "super",
                        Email = "super@apollos.edu",
                        EmailConfirmed=true,
                    };
                    var result = userMgr.CreateAsync(superAdmin, "P@ssw0rd").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    superAdmin = userMgr.FindByEmailAsync("super@apollos.edu").Result;

                    result = userMgr.AddClaimsAsync(superAdmin, new Claim[]{
                                new Claim(JwtClaimTypes.Name, "Super Admin"),
                                new Claim(JwtClaimTypes.GivenName, "Super"),
                                new Claim(JwtClaimTypes.FamilyName, "Admin"),
                                new Claim(JwtClaimTypes.Email, "super@apollos.edu"),
                                new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                                new Claim(JwtClaimTypes.WebSite, "http://apollos.edu"),
                                new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                            }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    Console.WriteLine("super administrator created");
                }
                else
                {
                    Console.WriteLine("super administrator already exists");
                }
            }

            {
                var context = provider.GetRequiredService<ConfigurationDbContext>();
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
                    foreach (var resource in Config.GetApis())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}