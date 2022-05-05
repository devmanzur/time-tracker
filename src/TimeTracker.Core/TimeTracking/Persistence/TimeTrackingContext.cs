using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Core.Auth.Utils;
using TimeTracker.Core.Shared.Exceptions;
using TimeTracker.Core.Shared.Interfaces;
using TimeTracker.Core.Shared.Persistence;
using TimeTracker.Core.Shared.Utils;
using TimeTracker.Core.TimeTracking.Interfaces;
using TimeTracker.Core.TimeTracking.Models.Entities;
using TimeTracker.Core.TimeTracking.Persistence.Configurations;

namespace TimeTracker.Core.TimeTracking.Persistence;

public class TimeTrackingContext : BaseDbContext<TimeTrackingContext>
{
    public TimeTrackingContext(DbContextOptions<TimeTrackingContext> options,
        IDomainEventsDispatcher domainEventsDispatcher, IHttpContextAccessor httpContextAccessor) : base(options,
        domainEventsDispatcher, httpContextAccessor)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<ActivityLabel> ActivityLabels { get; set; }
    public DbSet<Mandate> Mandates { get; set; }
    public DbSet<Activity> Activities { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("time-tracker");
        base.OnModelCreating(builder);

        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(IIndividuallyOwnedEntity).IsAssignableFrom(entityType.ClrType))
            {
                //TODO: disable individual id accessor when running migrations
                entityType.AddIndividuallyOwnedEntityQueryFilter("fudsahfjsdfksjdjkfhsdlkjdhl");
                // entityType.AddIndividuallyOwnedEntityQueryFilter(GetIndividualId());
            }
        }

        builder.ApplyConfiguration(new ActivityConfiguration());
        builder.ApplyConfiguration(new ActivityLabelConfiguration());
        builder.ApplyConfiguration(new CategoryConfiguration());
        builder.ApplyConfiguration(new MandateConfiguration());
    }

    private string GetIndividualId()
    {
        return HttpContextAccessor.HttpContext?.User?.GetClaimValue(CustomClaimTypes.IndividualId) ??
               throw new BusinessRuleViolationException(
                   "Individual Id missing");
    }

    private void SpecifyIndividual()
    {
        foreach (var entry in ChangeTracker.Entries<IIndividuallyOwnedEntity>().ToList())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.IndividualId = GetIndividualId();
                    break;
            }
        }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        SpecifyIndividual();
        return await base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        SpecifyIndividual();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        SpecifyIndividual();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new CancellationToken())
    {
        SpecifyIndividual();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}