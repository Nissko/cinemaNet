using Cinema.Application.DTO.Cinema;

namespace Cinema.Application.Application.Interfaces.Cinema;

public interface ICinemaRepository
{
    Task<IEnumerable<CinemaDto>> GetAllAsync();
    Task<CinemaDto?> GetByIdAsync(Guid id);
    Task<CinemaDto> CreateAsync(CreateCinemaDto dto);
    Task<bool> UpdateAsync(Guid id, UpdateCinemaDto dto);
    Task<bool> DeleteAsync(Guid id);
    Task<IEnumerable<CinemaDto>> SearchByNameAsync(string name);
}