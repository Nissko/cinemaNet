using Cinema.Domain.Aggregates.Users;
using Cinema.Domain.Common;

namespace Cinema.Domain.Aggregates.Cinemas
{
    /// <summary>
    /// Билет
    /// </summary>
    public class TicketEntity : Entity
    {
        public TicketEntity(DateTimeOffset purchaseDate, TicketStatus status, decimal price)
        {
            PurchaseDate = purchaseDate;
            Status = status;
            Price = price;
        }

        public TicketEntity(DateTimeOffset purchaseDate, TicketStatus status, decimal price, Guid screeningId, Guid seatId,
            Guid userId)
        {
            PurchaseDate = purchaseDate;
            Status = status;
            Price = price;
            ScreeningId = screeningId;
            SeatId = seatId;
            UserId = userId;
        }

        public DateTimeOffset PurchaseDate { get; private set; }
        public TicketStatus Status { get; private set; }
        public decimal Price { get; private set; }

        public Guid ScreeningId { get; private set; }
        public virtual ScreeningEntity ScreeningEntity { get; private set; }

        public Guid SeatId { get; private set; }
        public virtual SeatEntity SeatEntity { get; private set; }

        public Guid UserId { get; private set; }
        public virtual UserEntity UserEntity { get; private set; }

        public void UpdateStatus(TicketStatus? status)
        {
            Status = status ?? Status;
        }
    }
}
