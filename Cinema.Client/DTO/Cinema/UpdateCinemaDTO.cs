using System.ComponentModel.DataAnnotations;

namespace Cinema.Client.DTO.Cinema;

internal record UpdateCinemaDTO
{
    [StringLength(100)]
    public string Name { get; set; }
    
    [StringLength(200)]
    public string Address { get; set; }
}