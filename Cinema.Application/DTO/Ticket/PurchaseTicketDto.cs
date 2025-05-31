using System.ComponentModel.DataAnnotations;

namespace Cinema.Application.DTO.Ticket;

/// <summary>
/// Для покупки билета
/// </summary>
public record PurchaseTicketDto
{
    [Required]
    public Guid TicketId { get; set; }

    [Required]
    [CreditCard]
    public string PaymentCardNumber { get; set; } = string.Empty;
};