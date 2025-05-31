namespace Cinema.Application.DTO.Movie;

public record MovieDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Duration { get; set; }
    public decimal Rating { get; set; }
    public string ImagePath { get; set; }
};