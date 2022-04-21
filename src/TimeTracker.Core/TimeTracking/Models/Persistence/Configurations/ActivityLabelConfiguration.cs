using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeTracker.Core.TimeTracking.Models.Entities;

namespace TimeTracker.Core.TimeTracking.Models.Persistence.Configurations;

public class ActivityLabelConfiguration : IEntityTypeConfiguration<ActivityLabel>
{
    public void Configure(EntityTypeBuilder<ActivityLabel> builder)
    {
        builder.Property(x => x.Name).IsRequired();
        builder.HasIndex(x => x.Name).IsUnique();
        builder.Property(x => x.ColorCode).HasConversion<string>().IsRequired();
    }
}