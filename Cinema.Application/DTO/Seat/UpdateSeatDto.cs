using System.ComponentModel.DataAnnotations;
using Cinema.Domain.Aggregates.Cinemas;

namespace Cinema.Application.DTO.Seat
{
    public record UpdateSeatDto
    {
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        public SeatType Type { get; set; }
        public Guid AuditoriumId { get; set; }
    };
}