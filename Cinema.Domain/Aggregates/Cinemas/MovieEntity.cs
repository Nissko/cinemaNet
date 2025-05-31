using Cinema.Domain.Common;

namespace Cinema.Domain.Aggregates.Cinemas
{
    /// <summary>
    /// Фильм
    /// </summary>
    public class MovieEntity : Entity
    {
        public MovieEntity()
        {
            Screenings = new HashSet<ScreeningEntity>();
        }
        
        public MovieEntity(string title, string description, TimeSpan duration, decimal rating, string imagePath) : this()
        {
            Title = title;
            Description = description;
            Duration = duration;
            Rating = rating;
            ImagePath = imagePath;
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public TimeSpan Duration { get; private set; }
        public decimal Rating { get; private set; }
        public string ImagePath { get; private set; }

        public virtual ICollection<ScreeningEntity> Screenings { get; private set; }

        public void Update(string? title, string? description, TimeSpan? duration, decimal? rating, string? imagePath)
        {
            Title = title ?? Title;
            Description = description ?? Description;
            Duration = duration ?? Duration;
            Rating = rating ?? Rating;
            ImagePath = imagePath ?? ImagePath;
        }
    }
}
