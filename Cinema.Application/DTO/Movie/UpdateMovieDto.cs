using System.ComponentModel.DataAnnotations;

namespace Cinema.Application.DTO.Movie;

public record UpdateMovieDto
{
    [StringLength(150)]
    public string Title { get; set; }

    [StringLength(1000)]
    public string Description { get; set; }
    
    public TimeSpan Duration { get; set; }

    [Range(0, 10)]
    public decimal Rating { get; set; }
    public string ImagePath { get; set; }
};