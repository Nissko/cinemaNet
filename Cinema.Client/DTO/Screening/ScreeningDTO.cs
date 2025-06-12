using Cinema.Client.DTO.Auditorium;
using Cinema.Client.DTO.Movie;

namespace Cinema.Client.DTO.Screening
{
    internal record ScreeningDTO
    {
        public Guid Id { get; init; }
        public DateTimeOffset StartTime { get; set; } = DateTimeOffset.Now.ToOffset(TimeSpan.FromHours(5));
        public TimeSpan Duration { get; init; }
        public decimal Price { get; set; }
        public MovieDTO Movie { get; init; }
        public AuditoriumDTO Auditorium { get; init; } = new AuditoriumDTO();
    }
}