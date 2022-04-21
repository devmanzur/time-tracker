using TimeTracker.Core.Auth.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddict.Abstractions;

namespace TimeTracker.Core.Auth.Utils
{
    public class ClientSeedingService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public ClientSeedingService(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IdentityContext>();
            await context.Database.MigrateAsync(cancellationToken);
            await RegisterApplications(cancellationToken, scope);
            await RegisterScopes(scope);
        }

        private async Task RegisterScopes(IServiceScope scope)
        {
            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictScopeManager>();

            if (await manager.FindByNameAsync(LocalIdentityConfig.Scopes.SpaClient) == null)
            {
                var descriptor = new OpenIddictScopeDescriptor
                {
                    Name = LocalIdentityConfig.Scopes.SpaClient,
                    Resources =
                    {
                        LocalIdentityConfig.Resources.ResourceApi
                    }
                };

                await manager.CreateAsync(descriptor);
            }
        }

        private static async Task RegisterApplications(CancellationToken cancellationToken, IServiceScope scope)
        {
            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

            if (await manager.FindByClientIdAsync("postman", cancellationToken) is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "postman",
                    ClientSecret = "postman-secret",
                    DisplayName = "Postman Secret",
                    RedirectUris = { new Uri("https://oauth.pstmn.io/v1/callback") },
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Authorization,
                        OpenIddictConstants.Permissions.Endpoints.Token,

                        OpenIddictConstants.Permissions.GrantTypes.Password,
                        OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                        OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
                        OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                        OpenIddictConstants.Permissions.ResponseTypes.Code,
                        OpenIddictConstants.Permissions.Prefixes.Scope + LocalIdentityConfig.Scopes.SpaClient,
                    }
                }, cancellationToken);
            }

            if (await manager.FindByClientIdAsync("secure-spa-client2", cancellationToken) is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "secure-spa-client2",
                    ClientSecret = "secure-spa-client2-secret",
                    DisplayName = "Spa Client Secret",
                    RedirectUris = { new Uri("https://localhost:7186/login") },
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Authorization,
                        OpenIddictConstants.Permissions.Endpoints.Token,

                        OpenIddictConstants.Permissions.GrantTypes.Password,
                        OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                        OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
                        OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                        OpenIddictConstants.Permissions.ResponseTypes.Code,
                        OpenIddictConstants.Permissions.Prefixes.Scope + OpenIddictConstants.Scopes.OpenId,
                        OpenIddictConstants.Permissions.Prefixes.Scope + OpenIddictConstants.Scopes.Profile,
                        OpenIddictConstants.Permissions.Prefixes.Scope + OpenIddictConstants.Scopes.OfflineAccess,
                    }
                }, cancellationToken);
            }

            if (await manager.FindByClientIdAsync("oidc-debugger", cancellationToken) is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "oidc-debugger",
                    ClientSecret = "oidc-debugger-secret",
                    DisplayName = "Oidc debugger",
                    RedirectUris = { new Uri("https://oidcdebugger.com/debug") },
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Authorization,
                        OpenIddictConstants.Permissions.Endpoints.Token,

                        OpenIddictConstants.Permissions.GrantTypes.Password,
                        OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                        OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
                        OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                        OpenIddictConstants.Permissions.ResponseTypes.Code,
                        OpenIddictConstants.Permissions.Prefixes.Scope + LocalIdentityConfig.Scopes.SpaClient,
                    }
                }, cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}