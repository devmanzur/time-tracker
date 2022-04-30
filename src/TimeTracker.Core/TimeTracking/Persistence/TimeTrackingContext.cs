using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Core.Shared.Interfaces;
using TimeTracker.Core.Shared.Persistence;
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
        builder.ApplyConfiguration(new ActivityConfiguration());
        builder.ApplyConfiguration(new ActivityLabelConfiguration());
        builder.ApplyConfiguration(new CategoryConfiguration());
        builder.ApplyConfiguration(new MandateConfiguration());
    }
}