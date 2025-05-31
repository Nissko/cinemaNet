namespace Cinema.Client.DTO.Auditorium;

internal record AuditoriumDTO
{
    public Guid Id { get; set; }
    public int Number { get; set; }
    public int RowsCount { get; set; }
    public int SeatsPerRow { get; set; }
    public Guid CinemaId { get; set; }
}