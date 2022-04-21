using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Core.Shared.Interfaces;
using TimeTracker.Core.Shared.Persistence;
using TimeTracker.Core.TimeTracking.Models.Entities;
using TimeTracker.Core.TimeTracking.Models.Persistence.Configurations;

namespace TimeTracker.Core.TimeTracking.Models.Persistence;

public class TimeTrackingContext : BaseDbContext<TimeTrackingContext>
{
    public TimeTrackingContext(DbContextOptions<TimeTrackingContext> options,
        IDomainEventsDispatcher domainEventsDispatcher, IHttpContextAccessor httpContextAccessor) : base(options,
        domainEventsDispatcher, httpContextAccessor)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<ActivityLabel> ActivityLabels { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("time-tracker");
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ActivityLabelConfiguration());
        builder.ApplyConfiguration(new CategoryConfiguration());
    }
}