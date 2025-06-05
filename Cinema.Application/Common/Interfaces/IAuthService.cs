using Cinema.Domain.Aggregates.Users;

namespace Cinema.Application.Common.Interfaces
{
    public interface IAuthService
    {
        string GenerateJwtToken(UserEntity user);
    }
}