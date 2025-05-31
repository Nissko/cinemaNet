using Cinema.Domain.Aggregates.Cinemas;
using Cinema.Domain.Common;

namespace Cinema.Domain.Aggregates.Users
{
    /// <summary>
    /// Клиент
    /// </summary>
    public class UserEntity : Entity
    {
        public UserEntity()
        {
            Tickets = new HashSet<TicketEntity>();
        }
        
        public UserEntity(string email, string passwordHash) : this()
        {
            Email = email;
            PasswordHash = passwordHash;
            Role = "user";
        }
        
        public string Email { get; private set; }
        public string PasswordHash { get; set; }
        public string Role { get; private set; }

        public virtual ICollection<TicketEntity> Tickets { get; private set; } = new List<TicketEntity>();

        public void Update(string? email, string? passwordHash)
        {
            Email = email ?? Email;
            PasswordHash = passwordHash ?? PasswordHash;
        }
    }
}