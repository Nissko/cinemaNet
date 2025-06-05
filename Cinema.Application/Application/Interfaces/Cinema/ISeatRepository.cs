using Cinema.Application.DTO.Seat;

namespace Cinema.Application.Application.Interfaces.Cinema
{
    public interface ISeatRepository
    {
        Task<IEnumerable<SeatDto>> GetAllAsync();
        Task<SeatDto?> GetByIdAsync(Guid id);
        Task CreateAsync(CreateSeatDto dto);
        Task<bool> UpdateAsync(Guid id, UpdateSeatDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<SeatDto>> GetByAuditoriumIdAsync(Guid auditoriumId);
    }
}