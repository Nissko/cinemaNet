using Cinema.Application.Common.Interfaces;
using Cinema.Application.DTO.Movie;
using Microsoft.AspNetCore.SignalR;

namespace Cinema.Infrastructure.Services;

public class MovieHubService : IMovieHubService
{
    private readonly IHubContext<MovieHub> _hubContext;

    public MovieHubService(IHubContext<MovieHub> hubContext) => 
        _hubContext = hubContext;
    
    public async Task SendMovieUpdateAsync(MovieDto message)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveMovieUpdate", message);
    }

    public async Task SendMovieDeleteAsync(Guid movieId) =>
        await _hubContext.Clients.All.SendAsync("ReceiveMovieDelete", movieId);
}