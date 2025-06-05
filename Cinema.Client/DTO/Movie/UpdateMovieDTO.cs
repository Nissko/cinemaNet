using System.ComponentModel.DataAnnotations;

namespace Cinema.Client.DTO.Movie
{
    internal class UpdateMovieDto
    {
        [StringLength(150)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }
    
        public TimeSpan Duration { get; set; }

        [Range(0, 10)]
        public decimal Rating { get; set; }

        public string ImagePath { get; set; }
    }
}