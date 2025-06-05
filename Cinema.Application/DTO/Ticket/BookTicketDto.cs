using System.ComponentModel.DataAnnotations;

namespace Cinema.Application.DTO.Ticket
{
    /// <summary>
    /// Для бронирования билета
    /// </summary>
    public record BookTicketDto
    {
        [Required]
        public Guid ScreeningId { get; set; }

        [Required]
        public List<Guid> SeatId { get; set; }

        [Required]
        public string UserEmail { get; set; }
        [Required]
        public decimal Price { get; set; }
    };
}