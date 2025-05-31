using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Configuaration.Cinema;

public class CinemaConfiguration : IEntityTypeConfiguration<Domain.Aggregates.Cinemas.CinemaEntity>
{
    public void Configure(EntityTypeBuilder<Domain.Aggregates.Cinemas.CinemaEntity> builder)
    {
        builder.ToTable("Cinemas");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Address).IsRequired().HasMaxLength(200);
    }
}