using System.ComponentModel.DataAnnotations;
using Cinema.Domain.Aggregates.Cinemas;

namespace Cinema.Application.DTO.Seat
{
    public record CreateSeatDto
    {
        [Required]
        public int RowsCount { get; set; }
        [Required]
        public int SeatsCount { get; set; }
        [Required]
        public Guid AuditoriumId { get; set; }
    };
}