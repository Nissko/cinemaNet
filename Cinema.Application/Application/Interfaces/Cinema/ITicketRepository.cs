using Cinema.Application.DTO.Ticket;

namespace Cinema.Application.Application.Interfaces.Cinema;

public interface ITicketRepository
{
    Task<IEnumerable<TicketDto>> GetAllAsync();
    Task<TicketDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<TicketDto>> GetByScreeningIdAsync(Guid screeningId);
    Task<List<Guid>> BookTicketAsync(BookTicketDto dto);
    Task<TicketDto> PurchaseTicketAsync(PurchaseTicketDto dto);
    Task<bool> CancelTicketAsync(Guid id);
}