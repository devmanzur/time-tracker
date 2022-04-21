using System.Reflection;
using System.Security.Claims;
using TimeTracker.Core.Auth.Interfaces;
using TimeTracker.Core.Auth.Models.Entities;
using TimeTracker.Core.Auth.Utils;
using TimeTracker.Core.Shared.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using TimeTracker.Core.Auth.Persistence.Configurations;

namespace TimeTracker.Core.Auth.Persistence
{
    public class IdentityContext : IdentityDbContext<ApplicationUser, ApplicationRole, string,
        IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        private readonly IIdentityDomainEventsDispatcher _domainEventsDispatcher;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public DbSet<AccessPermission> AccessPermissions { get; set; }
        public DbSet<ApplicationRoleAccessPermission> RoleAccessPermissions { get; set; }
        

        public IdentityContext(DbContextOptions<IdentityContext> options, IConfiguration configuration,
            IIdentityDomainEventsDispatcher domainEventsDispatcher, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _domainEventsDispatcher = domainEventsDispatcher;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("auth");
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new AccessPermissionConfiguration());
            builder.ApplyConfiguration(new ApplicationRoleConfiguration());
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new ApplicationRoleAccessPermissionConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            Audit();
            var changes = TrackChanges();
            var changesMade = await base.SaveChangesAsync(cancellationToken);
            if (changesMade > 0) await _domainEventsDispatcher.DispatchEventsAsync(changes);
            return changesMade;
        }

        public override int SaveChanges()
        {
            Audit();
            var changes = TrackChanges();
            var changesMade = base.SaveChanges();
            if (changesMade > 0) _domainEventsDispatcher.DispatchEvents(changes);
            return changesMade;
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            Audit();
            var changes = TrackChanges();
            var changesMade = base.SaveChanges(acceptAllChangesOnSuccess);
            if (changesMade > 0) _domainEventsDispatcher.DispatchEvents(changes);
            return changesMade;
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken())
        {
            Audit();
            var changes = TrackChanges();
            var changesMade = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            if (changesMade > 0) await _domainEventsDispatcher.DispatchEventsAsync(changes);
            return changesMade;
        }

        public Task Save()
        {
            return this.SaveChangesAsync();
        }

        #region Change tracking

        private List<EntityEntry<IdentityAggregateRoot>> TrackChanges()
        {
            var changes = this.ChangeTracker
                .Entries<IdentityAggregateRoot>()
                .Where(x =>
                    HasDomainEvents(x) || HasBeenAddedOrRemoved(x)
                ).ToList();
            
            return changes;
        }

        private void Audit()
        {
            foreach (var entry in ChangeTracker.Entries<IAuditable>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedTime = DateTime.UtcNow;
                        entry.Entity.CreatedBy =
                            _httpContextAccessor.HttpContext?.User?.GetClaimValue(ClaimTypes.Name) ?? "Root";
                        entry.Entity.LastModifiedTime = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy =
                            _httpContextAccessor.HttpContext?.User?.GetClaimValue(ClaimTypes.Name) ?? "Root";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedTime = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy =
                            _httpContextAccessor.HttpContext?.User?.GetClaimValue(ClaimTypes.Name) ?? "Root";
                        break;
                }
            }
        }

        private static bool HasDomainEvents(EntityEntry<IdentityAggregateRoot> x)
        {
            return x.Entity.DomainEvents.Any();
        }

        private static bool HasBeenAddedOrRemoved(EntityEntry<IdentityAggregateRoot> x)
        {
            return x.State is EntityState.Added or EntityState.Deleted;
        }

        #endregion
    }
}