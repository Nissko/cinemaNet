using Cinema.Application.Common.Interfaces;
using Cinema.Application.DTO.Movie;
using Microsoft.AspNetCore.SignalR;

namespace Cinema.Infrastructure.Services
{
    public class MovieHub : Hub
    {
        private readonly IMovieHubService _movieHubService;

        public MovieHub(IMovieHubService movieHubService) => 
            _movieHubService = movieHubService;

        public async Task NotifyMovieUpdated(MovieDto message) =>
            await _movieHubService.SendMovieUpdateAsync(message);

        public async Task NotifyMovieDeleted(Guid movieId) =>
            await _movieHubService.SendMovieDeleteAsync(movieId);
    }
}