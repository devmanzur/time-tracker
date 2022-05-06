using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeTracker.Core.TimeTracking.Models.Entities;

namespace TimeTracker.Core.TimeTracking.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(x => x.Name).IsRequired();
        builder.HasIndex(x => new {x.Name, x.IndividualId}).IsUnique();
        builder.Property(x => x.IndividualId).IsRequired();
        builder.Property(x => x.IconUrl).IsRequired();
        builder.Property(x => x.ColorCode).HasConversion<string>().IsRequired();
        builder.Property(x => x.Priority).HasConversion<string>().IsRequired();
    }
}