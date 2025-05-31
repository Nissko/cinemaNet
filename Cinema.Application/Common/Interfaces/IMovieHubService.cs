using Cinema.Application.DTO.Movie;

namespace Cinema.Application.Common.Interfaces;

public interface IMovieHubService
{
    Task SendMovieUpdateAsync(MovieDto message);
    Task SendMovieDeleteAsync(Guid movieId);
}