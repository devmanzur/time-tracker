using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeTracker.Core.TimeTracking.Models.Entities;

namespace TimeTracker.Core.TimeTracking.Persistence.Configurations;

public class MandateConfiguration : IEntityTypeConfiguration<Mandate>
{
    public void Configure(EntityTypeBuilder<Mandate> builder)
    {
        builder.Property(x => x.Name).IsRequired();
        builder.HasIndex(x => x.Name).IsUnique();
        builder.Property(x => x.ColorCode).HasConversion<string>().IsRequired();
    }
}