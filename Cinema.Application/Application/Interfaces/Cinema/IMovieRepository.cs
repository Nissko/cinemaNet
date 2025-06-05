using Cinema.Application.DTO.Movie;

namespace Cinema.Application.Application.Interfaces.Cinema
{
    public interface IMovieRepository
    {
        Task<IEnumerable<MovieDto>> GetAllAsync();
        Task<MovieDto?> GetByIdAsync(Guid id);
        Task<MovieDto> CreateAsync(CreateMovieDto dto);
        Task<bool> UpdateAsync(Guid id, UpdateMovieDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}