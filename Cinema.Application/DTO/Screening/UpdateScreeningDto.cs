using System.ComponentModel.DataAnnotations;

namespace Cinema.Application.DTO.Screening
{
    public record UpdateScreeningDto
    {
        public DateTime StartTime { get; set; }
        public Guid MovieId { get; set; }
        public Guid AuditoriumId { get; set; }
        public decimal Price { get; set; }
    };
}