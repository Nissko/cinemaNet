using System.ComponentModel.DataAnnotations;

namespace Cinema.Client.DTO.Ticket
{
    internal record PurchaseTicketDTO
    {
        [Required]
        public Guid TicketId { get; set; }

        [Required]
        [CreditCard]
        public string PaymentCardNumber { get; set; } = string.Empty;
    };
}