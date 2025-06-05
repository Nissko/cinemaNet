using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Configuaration.User
{
    public class UserConfiguration : IEntityTypeConfiguration<Domain.Aggregates.Users.UserEntity>
    {
        public void Configure(EntityTypeBuilder<Domain.Aggregates.Users.UserEntity> builder)
        {
            builder.ToTable("Users");
    
            // Первичный ключ
            builder.HasKey(u => u.Id);
    
            // Свойства
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);
        
            builder.Property(u=>u.PasswordHash)
                .IsRequired()
                .HasMaxLength(100);
        
            builder.Property(u => u.Role)
                .IsRequired()
                .HasMaxLength(100)
                .HasDefaultValue("user");

            builder.HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}