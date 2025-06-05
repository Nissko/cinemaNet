using Cinema.Domain.Aggregates.Cinemas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Configuaration.Cinema
{
    public class SeatConfiguration : IEntityTypeConfiguration<SeatEntity>
    {
        public void Configure(EntityTypeBuilder<SeatEntity> builder)
        {
            builder.ToTable("Seats");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.RowNumber).IsRequired();
            builder.Property(s => s.SeatNumber).IsRequired();
        
            builder.Property(s => s.Type)
                .HasConversion<string>()
                .HasColumnName("Type");

            builder.HasOne(s => s.AuditoriumEntity)
                .WithMany(a => a.Seats)
                .HasForeignKey(s => s.AuditoriumId);
        }
    }
}