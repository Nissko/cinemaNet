using Cinema.Domain.Aggregates.Cinemas;
using Cinema.Domain.Aggregates.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Cinema.Application.Common.Interfaces
{
    public interface ICinemaDbContext
    {
        DatabaseFacade Database { get; }
    
        public DbSet<CinemaEntity> Cinema { get; set; }
        public DbSet<MovieEntity> Movie { get; set; }
        public DbSet<AuditoriumEntity> Auditorium { get; set; }
        public DbSet<ScreeningEntity> Screening { get; set; }
        public DbSet<SeatEntity> Seat { get; set; }
        public DbSet<TicketEntity> Ticket { get; set; }
        public DbSet<UserEntity> User { get; set; }
    
        void Migrate();
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}