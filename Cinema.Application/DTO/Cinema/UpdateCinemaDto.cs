using System.ComponentModel.DataAnnotations;

namespace Cinema.Application.DTO.Cinema;

public record UpdateCinemaDto
{
    [StringLength(100)]
    public string Name { get; set; }
    
    [StringLength(200)]
    public string Address { get; set; }
};