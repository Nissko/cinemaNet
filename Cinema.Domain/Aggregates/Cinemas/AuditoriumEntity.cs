using Cinema.Domain.Common;

namespace Cinema.Domain.Aggregates.Cinemas
{
    /// <summary>
    /// Зал
    /// </summary>
    public class AuditoriumEntity : Entity
    {
        public AuditoriumEntity()
        {
            Screenings = new HashSet<ScreeningEntity>();
            Seats = new HashSet<SeatEntity>();
        }
        
        public AuditoriumEntity(int number, int rowsCount, int seatsPerRow) : this()
        {
            Number = number;
            RowsCount = rowsCount;
            SeatsPerRow = seatsPerRow;
        }
        
        public AuditoriumEntity(int number, int rowsCount, int seatsPerRow, Guid cinemaId) : this()
        {
            Number = number;
            RowsCount = rowsCount;
            SeatsPerRow = seatsPerRow;
            CinemaId = cinemaId;
        }

        public int Number { get; private set; }
        public int RowsCount { get; private set; }
        public int SeatsPerRow { get; private set; }

        public Guid CinemaId { get; private set; }
        public virtual CinemaEntity CinemaEntity { get; private set; }

        public virtual ICollection<ScreeningEntity> Screenings { get; private set; }
        public virtual ICollection<SeatEntity> Seats { get; private set; }

        public void Update(int? number, int? rowsCount, int? seatsPerRow, Guid? cinemaId)
        {
            Number = number ?? Number;
            RowsCount = rowsCount ?? RowsCount;
            SeatsPerRow = seatsPerRow ?? SeatsPerRow;
            CinemaId = cinemaId ?? CinemaId;
        }
    }
}
