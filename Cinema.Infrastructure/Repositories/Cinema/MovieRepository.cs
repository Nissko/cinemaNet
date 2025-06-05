using Cinema.Application.Application.Interfaces.Cinema;
using Cinema.Application.Common.Interfaces;
using Cinema.Application.DTO.Movie;
using Cinema.Domain.Aggregates.Cinemas;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Repositories.Cinema
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ICinemaDbContext _context;

        public MovieRepository(ICinemaDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<MovieDto>> GetAllAsync()
        {
            var movie = await _context.Movie.ToListAsync();

            return movie.Select(m => new MovieDto
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                Duration = m.Duration.TotalMinutes.ToString(),
                Rating = m.Rating,
                ImagePath = m.ImagePath
            });
        }

        public async Task<MovieDto?> GetByIdAsync(Guid id)
        {
            var movie = await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);
            return movie == null
                ? null
                : new MovieDto
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Description = movie.Description,
                    Duration = movie.Duration.TotalMinutes.ToString(),
                    Rating = movie.Rating,
                    ImagePath = movie.ImagePath
                };
        }

        public async Task<MovieDto> CreateAsync(CreateMovieDto dto)
        {
            if (dto.Duration.TotalHours > 6)
            {
                throw new ArgumentException("Значение продолжительности фильма большое!");
            }
        
            var movie = new MovieEntity(dto.Title, dto.Description, dto.Duration, dto.Rating, dto.ImagePath);

            _context.Movie.Add(movie);
            await _context.SaveChangesAsync(CancellationToken.None);

            return new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                Duration = movie.Duration.TotalMinutes.ToString(),
                Rating = movie.Rating,
                ImagePath = movie.ImagePath
            };
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateMovieDto dto)
        {
            var movie = await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null) return false;

            movie.Update(dto.Title, dto.Description, dto.Duration, dto.Rating, dto.ImagePath);

            _context.Movie.Update(movie);
            await _context.SaveChangesAsync(CancellationToken.None);

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var movie = await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null) return false;

            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync(CancellationToken.None);

            return true;
        }
    }
}