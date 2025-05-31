using Cinema.Domain.Aggregates.Cinemas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Configuaration.Cinema;

public class AuditoriumConfiguration : IEntityTypeConfiguration<AuditoriumEntity>
{
    public void Configure(EntityTypeBuilder<AuditoriumEntity> builder)
    {
        builder.ToTable("Auditoriums");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Number).IsRequired();
        builder.HasIndex(a => a.Number).IsUnique();

        builder.HasOne(a => a.CinemaEntity)
            .WithMany(c => c.Auditoriums)
            .HasForeignKey(a => a.CinemaId);
    }
}