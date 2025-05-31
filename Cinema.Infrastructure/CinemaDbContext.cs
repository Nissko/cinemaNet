using Cinema.Application.Common.Interfaces;
using Cinema.Domain.Aggregates.Cinemas;
using Cinema.Domain.Aggregates.Users;
using Cinema.Infrastructure.Configuaration.Cinema;
using Cinema.Infrastructure.Configuaration.User;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure;

public class CinemaDbContext : DbContext, ICinemaDbContext
{
    protected readonly string _defaultSchema = "CINEMA_MAIN";

    public CinemaDbContext(DbContextOptions<CinemaDbContext> options)
        : base(options)
    { }
    
    public DbSet<CinemaEntity> Cinema { get; set; }
    public DbSet<MovieEntity> Movie { get; set; }
    public DbSet<AuditoriumEntity> Auditorium { get; set; }
    public DbSet<ScreeningEntity> Screening { get; set; }
    public DbSet<SeatEntity> Seat { get; set; }
    public DbSet<TicketEntity> Ticket { get; set; }
    public DbSet<UserEntity> User { get; set; }

    public void Migrate()
    {
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CinemaConfiguration());
        modelBuilder.ApplyConfiguration(new MovieConfiguration());
        modelBuilder.ApplyConfiguration(new AuditoriumConfiguration());
        modelBuilder.ApplyConfiguration(new ScreeningConfiguration());
        modelBuilder.ApplyConfiguration(new SeatConfiguration());
        modelBuilder.ApplyConfiguration(new TicketConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CinemaDbContext).Assembly);
    }

    public CinemaDbContext()
    {
        Database.EnsureCreated();
    }
        
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server=185.251.90.207;User Id=admin;Password=admin;Port=5432;Database=cinema_db;")
            .UseLazyLoadingProxies();
    }
        
    protected static DbContextOptions<T> ChangeOptionsType<T>(DbContextOptions options) where T : DbContext
    {
        return new DbContextOptionsBuilder<T>()
            .Options;
    }
}