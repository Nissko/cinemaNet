using System.ComponentModel.DataAnnotations;

namespace Cinema.Application.DTO.Ticket
{
    /// <summary>
    /// Для покупки билета
    /// </summary>
    public record PurchaseTicketDto
    {
        [Required]
        public List<Guid>? TicketId { get; init; }
        
        [Required]
        public string ToUserEmail { get; init; }
    };
}