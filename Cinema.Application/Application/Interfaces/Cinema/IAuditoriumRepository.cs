using Cinema.Application.DTO.Auditorium;

namespace Cinema.Application.Application.Interfaces.Cinema
{
    public interface IAuditoriumRepository
    {
        Task<IEnumerable<AuditoriumDto>> GetAllAsync();
        Task<AuditoriumDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<AuditoriumDto>> GetByCinemaIdAsync(Guid cinemaId);
        Task<AuditoriumDto> CreateAsync(CreateAuditoriumDto dto);
        Task<bool> UpdateAsync(Guid id, UpdateAuditoriumDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}