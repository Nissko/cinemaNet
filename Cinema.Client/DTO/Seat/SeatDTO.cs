namespace Cinema.Client.DTO.Seat;

internal record SeatDTO
{
    public Guid Id { get; set; }
    public int RowNumber { get; set; }
    public int SeatNumber { get; set; }
    public string Type { get; set; }
    public Guid AuditoriumId { get; set; }
}