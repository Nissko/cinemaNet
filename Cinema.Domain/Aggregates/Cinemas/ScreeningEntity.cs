using Cinema.Domain.Common;

namespace Cinema.Domain.Aggregates.Cinemas
{
    /// <summary>
    /// Сеанс
    /// </summary>
    public class ScreeningEntity : Entity
    {
        public ScreeningEntity()
        {
            Tickets = new HashSet<TicketEntity>();
        }

        public ScreeningEntity(DateTimeOffset startTime, Guid movieId, Guid auditoriumId, TimeSpan duration, decimal price) : this()
        {
            StartTime = startTime;
            MovieId = movieId;
            AuditoriumId = auditoriumId;
            Duration = duration;
            Price = price;
        }

        public DateTimeOffset StartTime { get; private set; }
        public TimeSpan Duration;
        public Guid AuditoriumId { get; private set; }
        public virtual AuditoriumEntity AuditoriumEntity { get; private set; }
        public Guid MovieId { get; private set; }
        public decimal Price { get; private set; }
        public virtual MovieEntity MovieEntity { get; private set; }
        public virtual ICollection<TicketEntity> Tickets { get; private set; }

        public void Update(DateTimeOffset? startTime, Guid? movieId, Guid? auditoriumId, Decimal? price)
        {
            StartTime = startTime ?? StartTime;
            MovieId = movieId ?? MovieId;
            AuditoriumId = auditoriumId ?? AuditoriumId;
            Duration = MovieEntity.Duration;
            Price = price ?? Price;
        }
    }
}
