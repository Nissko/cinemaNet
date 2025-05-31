using System.ComponentModel.DataAnnotations;

namespace Cinema.Client.DTO.Seat;

internal record CreateSeatDTO
{
    [Required]
    public int RowsCount { get; set; }
    [Required]
    public int SeatsCount { get; set; }
    [Required]
    public Guid AuditoriumId { get; set; }
}