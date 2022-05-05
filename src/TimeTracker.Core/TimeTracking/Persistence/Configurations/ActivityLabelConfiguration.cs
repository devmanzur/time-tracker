using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeTracker.Core.TimeTracking.Models.Entities;

namespace TimeTracker.Core.TimeTracking.Persistence.Configurations;

public class ActivityLabelConfiguration : IEntityTypeConfiguration<ActivityLabel>
{
    public void Configure(EntityTypeBuilder<ActivityLabel> builder)
    {
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.IndividualId).IsRequired();
        builder.HasIndex(x => x.Name).IsUnique();
        builder.Property(x => x.ColorCode).HasConversion<string>().IsRequired();
        
        
        
        builder.HasMany(a => a.Tags)
            .WithOne(r => r.ActivityLabel)
            .HasForeignKey(r => r.ActivityLabelId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Metadata.FindNavigation(nameof(ActivityLabel.Tags))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}