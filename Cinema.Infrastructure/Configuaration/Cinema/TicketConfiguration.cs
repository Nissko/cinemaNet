using Cinema.Domain.Aggregates.Cinemas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Configuaration.Cinema;

public class TicketConfiguration : IEntityTypeConfiguration<TicketEntity>
{
    public void Configure(EntityTypeBuilder<TicketEntity> builder)
    {
        builder.ToTable("Tickets");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.PurchaseDate).IsRequired();
        builder.Property(t => t.Price).HasColumnType("decimal(18,2)");

        builder.Property(t => t.Status)
            .HasConversion<string>()
            .HasColumnName("Status");

        builder.HasOne(t => t.ScreeningEntity)
            .WithMany(s => s.Tickets)
            .HasForeignKey(t => t.ScreeningId);

        builder.HasOne(t => t.SeatEntity)
            .WithMany(s => s.Tickets)
            .HasForeignKey(t => t.SeatId);

        builder.HasOne(t => t.UserEntity)
            .WithMany(u => u.Tickets)
            .HasForeignKey(t => t.UserId);
    }
}