using Cinema.Application.Application.Interfaces.Cinema;
using Cinema.Application.Common.Interfaces;
using Cinema.Application.DTO.Auditorium;
using Cinema.Application.DTO.Seat;
using Cinema.Domain.Aggregates.Cinemas;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Repositories.Cinema
{
    public class AuditoriumRepository : IAuditoriumRepository
    {
        private readonly ICinemaDbContext _context;
        private readonly ISeatRepository _seatRepository;

        public AuditoriumRepository(ICinemaDbContext context, ISeatRepository seatRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _seatRepository = seatRepository ?? throw new ArgumentNullException(nameof(seatRepository));
        }

        public async Task<IEnumerable<AuditoriumDto>> GetAllAsync()
        {
            var auditorium = await _context.Auditorium.ToListAsync();

            return auditorium.Select(a => new AuditoriumDto
            {
                Id = a.Id,
                Number = a.Number,
                RowsCount = a.RowsCount,
                SeatsPerRow = a.SeatsPerRow,
                CinemaId = a.CinemaId
            }).OrderBy(a => a.Number);
        }

        public async Task<AuditoriumDto?> GetByIdAsync(Guid id)
        {
            var auditorium = await _context.Auditorium.FirstOrDefaultAsync(a => a.Id == id);

            return auditorium == null
                ? null
                : new AuditoriumDto
                {
                    Id = auditorium.Id,
                    Number = auditorium.Number,
                    RowsCount = auditorium.RowsCount,
                    SeatsPerRow = auditorium.SeatsPerRow,
                    CinemaId = auditorium.CinemaId
                };
        }

        public async Task<IEnumerable<AuditoriumDto>> GetByCinemaIdAsync(Guid cinemaId)
        {
            var auditorium = await _context.Auditorium.Where(a => a.CinemaId == cinemaId).ToListAsync();

            return auditorium.Select(a => new AuditoriumDto
            {
                Id = a.Id,
                Number = a.Number,
                RowsCount = a.RowsCount,
                SeatsPerRow = a.SeatsPerRow,
                CinemaId = a.CinemaId
            }).OrderBy(a => a.Number);
        }

        public async Task<AuditoriumDto> CreateAsync(CreateAuditoriumDto dto)
        {
            var auditorium = new AuditoriumEntity(dto.Number, dto.RowsCount, dto.SeatsPerRow, dto.CinemaId);

            _context.Auditorium.Add(auditorium);
            await _context.SaveChangesAsync(CancellationToken.None);
        
            await _seatRepository.CreateAsync(new CreateSeatDto
            {
                AuditoriumId = auditorium.Id,
                RowsCount = auditorium.RowsCount,
                SeatsCount = auditorium.SeatsPerRow,
            });

            return new AuditoriumDto
            {
                Id = auditorium.Id,
                Number = auditorium.Number,
                RowsCount = auditorium.RowsCount,
                SeatsPerRow = auditorium.SeatsPerRow,
                CinemaId = auditorium.CinemaId
            };
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateAuditoriumDto dto)
        {
            var auditorium = await _context.Auditorium.FirstOrDefaultAsync(a => a.Id == id);
            if (auditorium == null) return false;

            auditorium.Update(dto.Number, dto.RowsCount, dto.SeatsPerRow, dto.CinemaId);

            _context.Auditorium.Update(auditorium);
            await _context.SaveChangesAsync(CancellationToken.None);

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var auditorium = await _context.Auditorium.FirstOrDefaultAsync(a => a.Id == id);
            if (auditorium == null) return false;

            if (auditorium.Screenings.Count > 0)
            {
                throw new Exception("Нельзя удалить зал т.к есть не начавшиеся сеансы");
            }
        
            await _seatRepository.DeleteAsync(auditorium.Id);

            _context.Auditorium.Remove(auditorium);
            await _context.SaveChangesAsync(CancellationToken.None);

            return true;
        }
    }
}