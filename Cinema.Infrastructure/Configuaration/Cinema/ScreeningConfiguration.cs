using Cinema.Domain.Aggregates.Cinemas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Configuaration.Cinema
{
    public class ScreeningConfiguration : IEntityTypeConfiguration<ScreeningEntity>
    {
        public void Configure(EntityTypeBuilder<ScreeningEntity> builder)
        {
            builder.ToTable("Screenings");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.StartTime).IsRequired();
            builder.Property(s=> s.Price).IsRequired();

            builder.HasOne(s => s.AuditoriumEntity)
                .WithMany(a => a.Screenings)
                .HasForeignKey(s => s.AuditoriumId);

            builder.HasOne(s => s.MovieEntity)
                .WithMany(m => m.Screenings)
                .HasForeignKey(s => s.MovieId);
        }
    }
}