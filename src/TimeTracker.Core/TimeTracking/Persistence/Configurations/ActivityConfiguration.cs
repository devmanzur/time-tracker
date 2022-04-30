using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeTracker.Core.TimeTracking.Models.Entities;

namespace TimeTracker.Core.TimeTracking.Persistence.Configurations;

public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.Property(x => x.Date).IsRequired();
        builder.Property(x => x.DurationInSeconds).IsRequired();
        builder.Property(x => x.CategoryId).IsRequired();
        builder.Property(x => x.MandateId).IsRequired();
        
        builder.HasMany(a => a.Tags)
            .WithOne(r => r.Activity)
            .HasForeignKey(r => r.ActivityId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Metadata.FindNavigation(nameof(Activity.Tags))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}