using Cinema.Application.DTO.Auditorium;
using Cinema.Application.DTO.Movie;

namespace Cinema.Application.DTO.Screening;

public record ScreeningDto
{
    public Guid Id { get; init; }
    public DateTime StartTime { get; init; }
    public TimeSpan Duration { get; init; }
    public decimal Price { get; init; }
    public MovieDto Movie { get; init; }
    public AuditoriumDto Auditorium { get; init; }
};