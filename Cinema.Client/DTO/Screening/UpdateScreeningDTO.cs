namespace Cinema.Client.DTO.Screening;

internal record UpdateScreeningDTO
{
    public DateTime StartTime { get; set; }
    public Guid MovieId { get; set; }
    public Guid AuditoriumId { get; set; }
    public decimal Price { get; set; }
}