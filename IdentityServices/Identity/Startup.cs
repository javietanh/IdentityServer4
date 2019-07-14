using IdentityServer4.EntityFramework.Options;
using IdentityServer4.Services;
using IdentityServices.Data;
using IdentityServices.Models;
using IdentityServices.Shared.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Identity.Shared.Certificate;
using Services.Identity.Shared.Services;
using System;
using System.Reflection;

namespace IdentityServices
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }
        
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("mysql.localhost");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            // change connection when using with docker
            if (Configuration.GetValue<bool>("UseDocker") == true)
            {
                connectionString = Configuration.GetConnectionString("mysql.container");
            }

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>(cf =>
            {
                // User settings
                cf.User.RequireUniqueEmail = true;
                //cf.User.AllowedUserNameCharacters = "";

                // Password settings
                cf.Password.RequireDigit = true;
                cf.Password.RequiredLength = 6;
                cf.Password.RequireLowercase = true;
                cf.Password.RequireNonAlphanumeric = true;
                cf.Password.RequireUppercase = true;

                // Lockout settings
                cf.Lockout.AllowedForNewUsers = true;
                cf.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                cf.Lockout.MaxFailedAccessAttempts = 3;

                // SignIn settings
                cf.SignIn.RequireConfirmedEmail = true;
                cf.SignIn.RequireConfirmedPhoneNumber = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);

            services.Configure<IISOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
            // this adds the config data from DB (clients, resources)
            .AddConfigurationStore(options =>
            {
                foreach (var p in options.GetType().GetProperties())
                {
                    if (p.PropertyType != typeof(TableConfiguration))
                        continue;
                    var o = p.GetGetMethod().Invoke(options, null);
                    var q = o.GetType().GetProperty("Name");
                    var tableName = q.GetMethod.Invoke(o, null) as string;
                    o.GetType().GetProperty("Name").SetMethod
                            .Invoke(o, new object[] { $"idn_{tableName.ToSnakeCase()}" });
                }

                // use mysql database to persisted data
                options.ConfigureDbContext = cf =>
                    cf.UseMySql(connectionString, opt => opt.MigrationsAssembly(migrationsAssembly));
            })
            // this adds the operational data from DB (codes, tokens, consents)
            .AddOperationalStore(options =>
            {
                foreach (var p in options.GetType().GetProperties())
                {
                    if (p.PropertyType != typeof(TableConfiguration))
                        continue;
                    var o = p.GetGetMethod().Invoke(options, null);
                    var q = o.GetType().GetProperty("Name");
                    var tableName = q.GetMethod.Invoke(o, null) as string;
                    o.GetType().GetProperty("Name").SetMethod
                            .Invoke(o, new object[] { $"idn_{tableName.ToSnakeCase()}" });
                }

                // use mysql database to persisted data
                options.ConfigureDbContext = cf =>
                    cf.UseMySql(connectionString, opt => opt.MigrationsAssembly(migrationsAssembly));

                // this enables automatic token cleanup. this is optional.
                options.EnableTokenCleanup = true;
                options.TokenCleanupInterval = 3000; // interval in seconds
            })
            .AddAspNetIdentity<ApplicationUser>()
            .AddProfileService<ProfileService>(); ;

            _ = Environment.IsDevelopment()
               ? builder.AddDeveloperSigningCredential()
               : builder.AddSigningCredential(certificate: Certificate.Get());

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "1015097700236-b11ilbm3eenafv7tqr8sgqk7p501qfkv.apps.googleusercontent.com";
                    options.ClientSecret = "PdNoRw34wZ5WASEqGFZO23Sx";
                });
            // dependency injections
            services.AddTransient<IProfileService, ProfileService>();
            services.AddSingleton(Configuration);
            services.AddHttpContextAccessor();
        }

        public void Configure(IApplicationBuilder app)
        {
            // this will do the initial DB population
            SeedData.InitializeDatabase(app);

            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseMvcWithDefaultRoute();
        }

    }
}