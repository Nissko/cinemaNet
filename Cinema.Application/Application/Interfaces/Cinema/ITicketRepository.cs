using Cinema.Application.DTO.Ticket;
using Cinema.Domain.Aggregates.Cinemas;

namespace Cinema.Application.Application.Interfaces.Cinema
{
    public interface ITicketRepository
    {
        Task<IEnumerable<TicketDto>> GetAllAsync();
        Task<IEnumerable<TicketDto>> GetByEmailAndStatus(string email, TicketStatus statusText);
        Task<TicketDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<TicketDto>> GetByScreeningIdAsync(Guid screeningId);
        Task<List<Guid>> BookTicketAsync(BookTicketDto dto);
        Task<bool> PurchaseTicketAsync(PurchaseTicketDto dto);
        Task<bool> CancelTicketAsync(Guid id);
    }
}