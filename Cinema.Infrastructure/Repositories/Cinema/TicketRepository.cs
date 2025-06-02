using Cinema.Application.Application.Interfaces.Cinema;
using Cinema.Application.Common.Interfaces;
using Cinema.Application.DTO.Ticket;
using Cinema.Domain.Aggregates.Cinemas;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Repositories.Cinema;

public class TicketRepository : ITicketRepository
{
    private readonly ICinemaDbContext _context;
    private readonly IEventsMainHubService _eventsMainHubService;

    public TicketRepository(ICinemaDbContext context, IEventsMainHubService eventsMainHubService)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _eventsMainHubService = eventsMainHubService ?? throw new ArgumentNullException(nameof(eventsMainHubService));
    }

    public async Task<IEnumerable<TicketDto>> GetAllAsync()
    {
        var ticket = await _context.Ticket.ToListAsync();

        return ticket.Select(t => new TicketDto
        {
            Id = t.Id,
            PurchaseDate = t.PurchaseDate,
            Status = t.Status.ToString(),
            Price = t.Price,
            ScreeningId = t.ScreeningId,
            SeatId = t.SeatId,
            UserId = t.UserId
        });
    }

    public async Task<TicketDto?> GetByIdAsync(Guid id)
    {
        var ticket = await _context.Ticket.FirstOrDefaultAsync(t => t.Id == id);
        return ticket == null
            ? null
            : new TicketDto
            {
                Id = ticket.Id,
                PurchaseDate = ticket.PurchaseDate,
                Status = ticket.Status.ToString(),
                Price = ticket.Price,
                ScreeningId = ticket.ScreeningId,
                SeatId = ticket.SeatId,
                UserId = ticket.UserId
            };
    }

    public async Task<IEnumerable<TicketDto>> GetByScreeningIdAsync(Guid screeningId)
    {
        var ticket = await _context.Ticket.Where(t => t.ScreeningId == screeningId).ToListAsync();

        return ticket.Select(t => new TicketDto
        {
            Id = t.Id,
            PurchaseDate = t.PurchaseDate,
            Status = t.Status.ToString(),
            Price = t.Price,
            ScreeningId = t.ScreeningId,
            SeatId = t.SeatId,
            UserId = t.UserId
        });
    }

    public async Task<List<Guid>> BookTicketAsync(BookTicketDto dto)
    {
        var price = dto.Price;
        var user = await _context.User.FirstOrDefaultAsync(t => t.Email == dto.UserEmail);
        if (user == null) throw new ArgumentException("Пользователь не найден");

        if (dto.SeatId.Count > 1)
        {
            var newTickets = new List<TicketEntity>();
            foreach (var seatsId in dto.SeatId)
            {
                var seats = await _context.Seat.FirstOrDefaultAsync(t => t.Id == seatsId);
                if (seats == null) throw new ArgumentException("Место не найдено");

                var tickets = new TicketEntity(DateTime.Now.ToUniversalTime(), TicketStatus.Reserved, price,
                    dto.ScreeningId,
                    seatsId, user.Id);

                newTickets.Add(tickets);
            }

            await _context.Ticket.AddRangeAsync(newTickets);
            await _context.SaveChangesAsync(CancellationToken.None);

            await _eventsMainHubService.SendSeatsUpdateAsync();
            return newTickets.Select(t => t.SeatId).ToList();
        }

        var seatId = dto.SeatId.FirstOrDefault();
        var seat = await _context.Seat.FirstOrDefaultAsync(t => t.Id == seatId);
        if (seat == null) throw new ArgumentException("Место не найдено");

        var ticket = new TicketEntity(DateTime.Now.ToUniversalTime(), TicketStatus.Reserved, price, dto.ScreeningId,
            seatId, user.Id);

        await _context.Ticket.AddAsync(ticket);
        await _context.SaveChangesAsync(CancellationToken.None);

        await _eventsMainHubService.SendSeatsUpdateAsync();
        return [ticket.Id];
    }

    public async Task<TicketDto> PurchaseTicketAsync(PurchaseTicketDto dto)
    {
        var ticket = await _context.Ticket.FirstOrDefaultAsync(t => t.Id == dto.TicketId);
        if (ticket == null) throw new ArgumentException("Билет не найден");

        ticket.UpdateStatus(TicketStatus.Purchased);

        _context.Ticket.Update(ticket);
        await _context.SaveChangesAsync(CancellationToken.None);

        return new TicketDto
        {
            Id = ticket.Id,
            PurchaseDate = ticket.PurchaseDate,
            Status = ticket.Status.ToString(),
            Price = ticket.Price,
            ScreeningId = ticket.ScreeningId,
            SeatId = ticket.SeatId,
            UserId = ticket.UserId
        };
    }

    public async Task<bool> CancelTicketAsync(Guid id)
    {
        var ticket = await _context.Ticket.FirstOrDefaultAsync(t => t.Id == id);
        if (ticket == null) return false;

        ticket.UpdateStatus(TicketStatus.Cancelled);

        _context.Ticket.Update(ticket);
        await _context.SaveChangesAsync(CancellationToken.None);

        return true;
    }
}