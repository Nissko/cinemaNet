using System.ComponentModel.DataAnnotations;

namespace Cinema.Client.DTO.Ticket
{
    internal record PurchaseTicketDTO
    {
        [Required]
        public List<Guid>? TicketId { get; init; }
        
        [Required]
        public string ToUserEmail { get; init; }
    };
}