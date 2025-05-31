namespace Cinema.Client.DTO.Cinema;

internal record CinemaDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}