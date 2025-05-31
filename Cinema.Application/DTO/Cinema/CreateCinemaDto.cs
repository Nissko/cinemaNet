using System.ComponentModel.DataAnnotations;

namespace Cinema.Application.DTO.Cinema;

public record CreateCinemaDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Address { get; set; } = string.Empty;
};