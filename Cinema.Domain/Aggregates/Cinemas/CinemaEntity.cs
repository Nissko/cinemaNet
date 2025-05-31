using Cinema.Domain.Common;

namespace Cinema.Domain.Aggregates.Cinemas
{
    /// <summary>
    /// Кинотеатр
    /// </summary>
    public class CinemaEntity : Entity
    {
        public CinemaEntity()
        {
            Auditoriums = new HashSet<AuditoriumEntity>();
        }
        
        public CinemaEntity(string name, string address) : this()
        {
            Name = name;
            Address = address;
        }

        public string Name { get; private set; }
        public string Address { get; private set; }
        public virtual ICollection<AuditoriumEntity> Auditoriums { get; private set; }
        
        public void UpdateName(string? name) => Name = name ?? Name;
        public void UpdateAddress(string? address) => Address = address ?? Address;
    }
}
