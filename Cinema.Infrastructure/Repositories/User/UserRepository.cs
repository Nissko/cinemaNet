using Cinema.Application.Application.Interfaces.User;
using Cinema.Application.Common.Interfaces;
using Cinema.Application.DTO.User;
using Cinema.Domain.Aggregates.Users;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly ICinemaDbContext _context;

        public UserRepository(ICinemaDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /*TODO: Делалось для пользаков, но используется проще функционал...*/
        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var user = await _context.User.ToListAsync();

            return user.Select(u => new UserDto
            {
                Id = u.Id,
                Email = u.Email
            });
        }

        public async Task<UserDto?> GetByIdAsync(Guid id)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == id);
            return user == null
                ? null
                : new UserDto
                {
                    Id = user.Id,
                    Email = user.Email
                };
        }

        public async Task<UserDto> CreateAsync(CreateUserDto dto)
        {
            var user = new UserEntity(dto.Email, dto.Password);

            _context.User.Add(user);
            await _context.SaveChangesAsync(CancellationToken.None);

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email
            };
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateUserDto dto)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return false;

            user.Update(dto.Email, dto.Password);

            _context.User.Update(user);
            await _context.SaveChangesAsync(CancellationToken.None);

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return false;

            _context.User.Remove(user);
            await _context.SaveChangesAsync(CancellationToken.None);

            return true;
        }
    }
}