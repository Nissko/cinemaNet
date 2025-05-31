using Cinema.Application.Application.Interfaces.Cinema;
using Cinema.Application.Common.Interfaces;
using Cinema.Application.DTO.Cinema;
using Cinema.Domain.Aggregates.Cinemas;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Repositories.Cinema;

public class CinemaRepository : ICinemaRepository
{
    private readonly ICinemaDbContext _context;

    public CinemaRepository(ICinemaDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<CinemaDto>> GetAllAsync()
    {
        var cinema = await _context.Cinema.ToListAsync();
        return cinema.Select(c => new CinemaDto
        {
            Id = c.Id,
            Name = c.Name,
            Address = c.Address
        });
    }

    public async Task<CinemaDto?> GetByIdAsync(Guid id)
    {
        var cinema = await _context.Cinema.FirstOrDefaultAsync(c => c.Id == id);
        return cinema == null
            ? null
            : new CinemaDto
            {
                Id = cinema.Id,
                Name = cinema.Name,
                Address = cinema.Address
            };
    }

    public async Task<CinemaDto> CreateAsync(CreateCinemaDto dto)
    {
        var cinema = new CinemaEntity(dto.Name, dto.Address);

        _context.Cinema.Add(cinema);
        await _context.SaveChangesAsync(CancellationToken.None);

        return new CinemaDto
        {
            Id = cinema.Id,
            Name = cinema.Name,
            Address = cinema.Address
        };
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateCinemaDto dto)
    {
        var cinema = await _context.Cinema.FirstOrDefaultAsync(c => c.Id == id);
        if (cinema == null) return false;

        cinema.UpdateName(dto.Name);
        cinema.UpdateAddress(dto.Address);

        _context.Cinema.Update(cinema);
        await _context.SaveChangesAsync(CancellationToken.None);

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var cinema = await _context.Cinema.FirstOrDefaultAsync(c => c.Id == id);
        if (cinema == null) return false;

        _context.Cinema.Remove(cinema);
        await _context.SaveChangesAsync(CancellationToken.None);
        return true;
    }

    public async Task<IEnumerable<CinemaDto>> SearchByNameAsync(string name)
    {
        var cinema = await _context.Cinema.Where(t => t.Name == name).ToListAsync();
        return cinema.Select(c => new CinemaDto
        {
            Id = c.Id,
            Name = c.Name,
            Address = c.Address
        });
    }
}