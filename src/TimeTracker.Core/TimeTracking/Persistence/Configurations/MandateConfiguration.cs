using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeTracker.Core.TimeTracking.Models.Entities;

namespace TimeTracker.Core.TimeTracking.Persistence.Configurations;

public class MandateConfiguration : IEntityTypeConfiguration<Mandate>
{
    public void Configure(EntityTypeBuilder<Mandate> builder)
    {
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.IndividualId).IsRequired();
        builder.HasIndex(x => new {x.Name, x.IndividualId}).IsUnique();
        builder.Property(x => x.ColorCode).HasConversion<string>().IsRequired();
    }
}