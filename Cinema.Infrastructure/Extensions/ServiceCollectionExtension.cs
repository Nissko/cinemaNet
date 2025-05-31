using Cinema.Application.Application.Extensions;
using Cinema.Application.Application.Interfaces.Cinema;
using Cinema.Application.Application.Interfaces.User;
using Cinema.Application.Common.Interfaces;
using Cinema.Infrastructure.Repositories.Cinema;
using Cinema.Infrastructure.Repositories.User;
using Cinema.Infrastructure.Services;
using Cinema.Infrastructure.Services.MainPageHub;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cinema.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCinemaCollectionInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        services.AddDbContext<CinemaDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PostgreSqlDatabase")));
            
        services.AddScoped<ICinemaDbContext>(provider => provider.GetService<CinemaDbContext>());
        services.AddScoped<IAuditoriumRepository, AuditoriumRepository>();
        services.AddScoped<ICinemaRepository, CinemaRepository>();
        services.AddScoped<IScreeningRepository, ScreeningRepository>();
        services.AddScoped<ISeatRepository, SeatRepository>();
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
            
        services.AddApplication();

        services.AddSingleton<IMovieHubService, MovieHubService>();
        services.AddSingleton<IEventsMainHubService, EventsMainHubService>();
        services.AddSingleton<IAuthService, AuthService>();
        
        return services;
    }
}