using System.ComponentModel.DataAnnotations;

namespace Cinema.Application.DTO.Movie;

public record CreateMovieDto
{
    [Required]
    [StringLength(150)]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public TimeSpan Duration { get; set; }

    [Range(0, 10)]
    public decimal Rating { get; set; }
    
    [Required]
    public string ImagePath { get; set; }
}