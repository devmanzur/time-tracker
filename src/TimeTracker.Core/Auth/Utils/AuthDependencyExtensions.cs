using TimeTracker.Core.Auth.Brokers;
using TimeTracker.Core.Auth.Interfaces;
using TimeTracker.Core.Auth.Models.Entities;
using TimeTracker.Core.Auth.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;

namespace TimeTracker.Core.Auth.Utils;

public static class AuthDependencyExtensions
{
    public static void AddAuthenticationModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IIdentityDomainEventsDispatcher, IdentityDomainEventsDispatcher>();
        services.AddScoped<ITokenNotificationBroker, DummyNotificationBroker>();
        services.AddHostedService<ClientSeedingService>();

        #region identity configuration

        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.ClaimsIdentity.UserNameClaimType = OpenIddictConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIddictConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIddictConstants.Claims.Role;
                options.ClaimsIdentity.EmailClaimType = OpenIddictConstants.Claims.Email;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = false;
                options.SignIn.RequireConfirmedEmail = false;
            })
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();

        services.Configure<DataProtectionTokenProviderOptions>(opt =>
            opt.TokenLifespan = TimeSpan.FromMinutes(5));

        #endregion

        #region openiddict setup

        services.AddOpenIddict()
            .AddCore(options =>
            {
                options.UseEntityFrameworkCore()
                    .UseDbContext<IdentityContext>();
            }).AddServer(options =>
            {
                // Enable the authorization, logout, token and userinfo endpoints.
                options.SetAuthorizationEndpointUris("/connect/authorize")
                    .SetLogoutEndpointUris("/connect/logout")
                    .SetTokenEndpointUris("/connect/token")
                    .SetIntrospectionEndpointUris("/connect/introspect")
                    .SetUserinfoEndpointUris("/connect/userinfo");


                // Mark the "email", "profile" and "roles" scopes as supported scopes.
                options.RegisterScopes(OpenIddictConstants.Scopes.Email, OpenIddictConstants.Scopes.Profile,
                    OpenIddictConstants.Scopes.Roles);

                options.AllowAuthorizationCodeFlow().RequireProofKeyForCodeExchange();
                options.AllowClientCredentialsFlow();
                options.AllowPasswordFlow();
                options.AllowRefreshTokenFlow();

                // Accept anonymous clients (i.e clients that don't send a client_id).
                options.AcceptAnonymousClients();

                // Register the signing and encryption credentials.
                options.AddDevelopmentEncryptionCertificate();
                //     .AddDevelopmentSigningCertificate();

                // Encryption and signing of tokens
                options.AddEphemeralEncryptionKey()
                    .AddEphemeralSigningKey();

                options.RegisterScopes(LocalIdentityConfig.Scopes.SpaClient);

                options.SetAccessTokenLifetime(TimeSpan.FromHours(1));
                options.SetIdentityTokenLifetime(TimeSpan.FromHours(1));
                options.SetRefreshTokenLifetime(TimeSpan.FromDays(1));

                // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
                options.UseAspNetCore()
                    //todo remove the disable transport layer security
                    .DisableTransportSecurityRequirement()
                    .EnableAuthorizationEndpointPassthrough()
                    .EnableLogoutEndpointPassthrough()
                    .EnableTokenEndpointPassthrough()
                    .EnableUserinfoEndpointPassthrough()
                    .EnableStatusCodePagesIntegration();

                options.DisableAccessTokenEncryption();
            })
            .AddValidation(options =>
            {
                // Import the configuration from the local OpenIddict server instance.
                options.UseLocalServer();

                // For applications that need immediate access token or authorization
                // revocation, the database entry of the received tokens and their
                // associated authorizations can be validated for each API call.
                // Enabling these options may have a negative impact on performance.
                options.EnableAuthorizationEntryValidation();
                options.EnableTokenEntryValidation();
            });

        #endregion

        #region identity store

        services.AddDbContext<IdentityContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("ApplicationDatabase"));
            options.UseOpenIddict();
        });

        #endregion
    }
}