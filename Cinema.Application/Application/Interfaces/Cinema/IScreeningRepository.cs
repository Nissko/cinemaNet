using Cinema.Application.DTO.Screening;

namespace Cinema.Application.Application.Interfaces.Cinema;

public interface IScreeningRepository
{
    Task<IEnumerable<ScreeningDto>> GetAllAsync();
    Task<IEnumerable<ScreeningDto>> GetByDayAsync(DateTime date);
    Task<ScreeningDto?> GetByIdAsync(Guid id);
    Task<ScreeningDto> CreateAsync(CreateScreeningDto dto);
    Task<bool> UpdateAsync(Guid id, UpdateScreeningDto dto);
    Task<bool> DeleteAsync(Guid id);
    Task<IEnumerable<ScreeningDto>> GetByMovieIdAsync(Guid movieId);
    Task<IEnumerable<ScreeningDto>> GetByAuditoriumIdAsync(Guid auditoriumId);
}