namespace Cinema.Application.DTO.Cinema;

public record CinemaDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
};