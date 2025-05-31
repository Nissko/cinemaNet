using Cinema.Application.Application.Interfaces.Cinema;
using Cinema.Application.Common.Interfaces;
using Cinema.Application.DTO.Auditorium;
using Cinema.Application.DTO.Movie;
using Cinema.Application.DTO.Screening;
using Cinema.Domain.Aggregates.Cinemas;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Repositories.Cinema;

public class ScreeningRepository : IScreeningRepository
{
    private readonly ICinemaDbContext _context;

    public ScreeningRepository(ICinemaDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<ScreeningDto>> GetAllAsync()
    {
        var screenings = await _context.Screening.ToListAsync();
        return screenings.Select(s => new ScreeningDto
        {
            Id = s.Id,
            StartTime = s.StartTime,
            Duration = s.Duration,
            Price = s.Price,
            Movie = new MovieDto
            {
                Id = s.MovieEntity.Id,
                Title = s.MovieEntity.Title,
                Description = s.MovieEntity.Description,
                Duration = s.MovieEntity.Duration.TotalMinutes.ToString(),
                Rating = s.MovieEntity.Rating,
                ImagePath = s.MovieEntity.ImagePath
            },
            Auditorium = new AuditoriumDto
            {
                Id = s.AuditoriumEntity.Id,
                Number = s.AuditoriumEntity.Number,
                RowsCount = s.AuditoriumEntity.RowsCount,
                SeatsPerRow = s.AuditoriumEntity.SeatsPerRow,
                CinemaId = s.AuditoriumEntity.CinemaId
            }
        });
    }

    public async Task<ScreeningDto?> GetByIdAsync(Guid id)
    {
        var screening = await _context.Screening.FirstOrDefaultAsync(s => s.Id == id);
        return screening == null
            ? null
            : new ScreeningDto
            {
                Id = screening.Id,
                StartTime = screening.StartTime,
                Duration = screening.Duration,
                Price = screening.Price,
                Movie = new MovieDto
                {
                    Id = screening.MovieEntity.Id,
                    Title = screening.MovieEntity.Title,
                    Description = screening.MovieEntity.Description,
                    Duration = screening.MovieEntity.Duration.TotalMinutes.ToString(),
                    Rating = screening.MovieEntity.Rating,
                    ImagePath = screening.MovieEntity.ImagePath
                },
                Auditorium = new AuditoriumDto
                {
                    Id = screening.AuditoriumEntity.Id,
                    Number = screening.AuditoriumEntity.Number,
                    RowsCount = screening.AuditoriumEntity.RowsCount,
                    SeatsPerRow = screening.AuditoriumEntity.SeatsPerRow,
                    CinemaId = screening.AuditoriumEntity.CinemaId
                }
            };
    }

    public async Task<ScreeningDto> CreateAsync(CreateScreeningDto dto)
    {
        var movie = await _context.Movie.FirstOrDefaultAsync(m => m.Id == dto.MovieId);
        var auditorium = await _context.Auditorium.FirstOrDefaultAsync(a => a.Id == dto.AuditoriumId);

        if (movie == null || auditorium == null)
            throw new ArgumentException("Фильм или зал не найден");

        var newStartTime = dto.StartTime;
        var newEndTime = newStartTime.AddMinutes(movie.Duration.TotalMinutes);

        var conflictValid = await _context.Screening
            .Where(s => s.AuditoriumId == dto.AuditoriumId)
            .Where(s => 
                (newStartTime < s.StartTime.AddMinutes(s.MovieEntity.Duration.TotalMinutes) && 
                 newEndTime > s.StartTime))
            .AnyAsync();

        if (conflictValid)
            throw new Exception("Показ пересекается по времени с другим в этом зале");

        if (dto.StartTime.Date < DateTime.Now.Date)
            throw new Exception(
                $"Нельзя создать показ раньше, чем {DateTime.Now.Date.ToString("dd.MM.yy")}");

        if (dto.StartTime.Date == DateTime.Now.Date && dto.StartTime.Hour < DateTime.Now.Hour &&
            dto.StartTime.Minute < DateTime.Now.Minute) 
            throw new Exception(
                $"Нельзя создать новый показ раньше, чем {DateTime.Now.ToString("HH:mm")}");

        var screening = new ScreeningEntity(dto.StartTime, dto.MovieId, dto.AuditoriumId, movie.Duration, dto.Price);

        _context.Screening.Add(screening);
        await _context.SaveChangesAsync(CancellationToken.None);

        return new ScreeningDto
        {
            Id = screening.Id,
            StartTime = screening.StartTime,
            Duration = screening.Duration,
            Price = screening.Price,
            Movie = new MovieDto
            {
                Id = screening.MovieEntity.Id,
                Title = screening.MovieEntity.Title,
                Description = screening.MovieEntity.Description,
                Duration = screening.MovieEntity.Duration.TotalMinutes.ToString(),
                Rating = screening.MovieEntity.Rating,
                ImagePath = screening.MovieEntity.ImagePath
            },
            Auditorium = new AuditoriumDto
            {
                Id = screening.AuditoriumEntity.Id,
                Number = screening.AuditoriumEntity.Number,
                RowsCount = screening.AuditoriumEntity.RowsCount,
                SeatsPerRow = screening.AuditoriumEntity.SeatsPerRow,
                CinemaId = screening.AuditoriumEntity.CinemaId
            }
        };
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateScreeningDto dto)
    {
        var screening = await _context.Screening.FirstOrDefaultAsync(s => s.Id == id);
        if (screening == null) return false;

        var movie = await _context.Movie.FirstOrDefaultAsync(m => m.Id == dto.MovieId);
        var auditorium = await _context.Auditorium.FirstOrDefaultAsync(a => a.Id == dto.AuditoriumId);
        if (movie == null || auditorium == null)
            throw new ArgumentException("Фильм или зал не найден");
        
        var newStartTime = dto.StartTime;
        var newEndTime = newStartTime.AddMinutes(movie.Duration.TotalMinutes);

        var conflictValid = await _context.Screening
            .Where(s => s.AuditoriumId == dto.AuditoriumId)
            .Where(s => 
                (newStartTime < s.StartTime.AddMinutes(s.MovieEntity.Duration.TotalMinutes) && 
                 newEndTime > s.StartTime))
            .Where(s=>s.MovieId != dto.MovieId)
            .AnyAsync();

        if (conflictValid)
            throw new Exception("Показ пересекается по времени с другим в этом зале");

        screening.Update(dto.StartTime.ToUniversalTime(), dto.MovieId, dto.AuditoriumId, dto.Price);

        _context.Screening.Update(screening);
        await _context.SaveChangesAsync(CancellationToken.None);

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var screening = await _context.Screening.FirstOrDefaultAsync(s => s.Id == id);
        if (screening == null) return false;

        _context.Screening.Remove(screening);
        await _context.SaveChangesAsync(CancellationToken.None);

        return true;
    }

    public async Task<IEnumerable<ScreeningDto>> GetByMovieIdAsync(Guid movieId)
    {
        var screenings = await _context.Screening.Where(s => s.MovieId == movieId).ToListAsync();
        return screenings.Select(s => new ScreeningDto
        {
            Id = s.Id,
            StartTime = s.StartTime,
            Duration = s.Duration,
            Price = s.Price,
            Movie = new MovieDto
            {
                Id = s.MovieEntity.Id,
                Title = s.MovieEntity.Title,
                Description = s.MovieEntity.Description,
                Duration = s.MovieEntity.Duration.TotalMinutes.ToString(),
                Rating = s.MovieEntity.Rating,
                ImagePath = s.MovieEntity.ImagePath
            },
            Auditorium = new AuditoriumDto
            {
                Id = s.AuditoriumEntity.Id,
                Number = s.AuditoriumEntity.Number,
                RowsCount = s.AuditoriumEntity.RowsCount,
                SeatsPerRow = s.AuditoriumEntity.SeatsPerRow,
                CinemaId = s.AuditoriumEntity.CinemaId
            }
        });
    }

    public async Task<IEnumerable<ScreeningDto>> GetByAuditoriumIdAsync(Guid auditoriumId)
    {
        var screenings = await _context.Screening.Where(s => s.AuditoriumId == auditoriumId).ToListAsync();
        return screenings.Select(s => new ScreeningDto
        {
            Id = s.Id,
            StartTime = s.StartTime,
            Duration = s.Duration,
            Price = s.Price,
            Movie = new MovieDto
            {
                Id = s.MovieEntity.Id,
                Title = s.MovieEntity.Title,
                Description = s.MovieEntity.Description,
                Duration = s.MovieEntity.Duration.TotalMinutes.ToString(),
                Rating = s.MovieEntity.Rating,
                ImagePath = s.MovieEntity.ImagePath
            },
            Auditorium = new AuditoriumDto
            {
                Id = s.AuditoriumEntity.Id,
                Number = s.AuditoriumEntity.Number,
                RowsCount = s.AuditoriumEntity.RowsCount,
                SeatsPerRow = s.AuditoriumEntity.SeatsPerRow,
                CinemaId = s.AuditoriumEntity.CinemaId
            }
        });
    }
}