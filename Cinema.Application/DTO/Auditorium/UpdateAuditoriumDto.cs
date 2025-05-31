using System.ComponentModel.DataAnnotations;

namespace Cinema.Application.DTO.Auditorium;

public record UpdateAuditoriumDto
{
    [Required]
    public int Number { get; set; }

    [Required]
    public int RowsCount { get; set; }

    [Required]
    public int SeatsPerRow { get; set; }

    [Required]
    public Guid CinemaId { get; set; }
};