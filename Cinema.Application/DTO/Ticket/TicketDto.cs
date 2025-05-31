namespace Cinema.Application.DTO.Ticket;

public record TicketDto
{
    public Guid Id { get; set; }
    public DateTime PurchaseDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Guid ScreeningId { get; set; }
    public Guid SeatId { get; set; }
    public Guid UserId { get; set; }
};