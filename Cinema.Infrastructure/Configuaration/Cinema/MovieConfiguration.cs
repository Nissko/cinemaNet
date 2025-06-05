using Cinema.Domain.Aggregates.Cinemas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Configuaration.Cinema
{
    public class MovieConfiguration : IEntityTypeConfiguration<MovieEntity>
    {
        public void Configure(EntityTypeBuilder<MovieEntity> builder)
        {
            builder.ToTable("Movies");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Title).IsRequired().HasMaxLength(150);
            builder.Property(m => m.Description).HasMaxLength(1000);
            builder.Property(m => m.Rating).HasPrecision(3, 1);
            builder.Property(m => m.ImagePath);
        }
    }
}