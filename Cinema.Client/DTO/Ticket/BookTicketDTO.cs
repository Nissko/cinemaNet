using System.ComponentModel.DataAnnotations;

namespace Cinema.Client.DTO.Ticket;

internal record BookTicketDTO
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