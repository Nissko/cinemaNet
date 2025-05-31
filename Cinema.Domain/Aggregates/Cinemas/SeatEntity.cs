using Cinema.Domain.Common;

namespace Cinema.Domain.Aggregates.Cinemas
{
    /// <summary>
    /// Место
    /// </summary>
    public class SeatEntity : Entity
    {
        public SeatEntity()
        {
            Tickets = new HashSet<TicketEntity>();
        }
        
        public SeatEntity(int rowNumber, int seatNumber, SeatType type, Guid auditoriumId) : this()
        {
            RowNumber = rowNumber;
            SeatNumber = seatNumber;
            Type = type;
            AuditoriumId = auditoriumId;
        }

        public int RowNumber { get; private set; }
        public int SeatNumber { get; private set; }
        public SeatType Type { get; private set; }
        public Guid AuditoriumId { get; private set; }
        public virtual AuditoriumEntity AuditoriumEntity { get; private set; }
        public virtual ICollection<TicketEntity> Tickets { get; private set; }

        public void Update(int? rowNumber, int? seatNumber, SeatType? type, Guid? auditoriumId)
        {
            RowNumber = rowNumber ?? RowNumber;
            SeatNumber = seatNumber ?? SeatNumber;
            Type = type ?? Type;
            AuditoriumId = auditoriumId ?? AuditoriumId;
        }
    }
}
