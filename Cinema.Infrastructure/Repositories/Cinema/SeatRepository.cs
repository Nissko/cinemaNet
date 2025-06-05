using Cinema.Application.Application.Interfaces.Cinema;
using Cinema.Application.Common.Interfaces;
using Cinema.Application.DTO.Seat;
using Cinema.Domain.Aggregates.Cinemas;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Repositories.Cinema
{
    public class SeatRepository : ISeatRepository
    {
        private readonly ICinemaDbContext _context;

        public SeatRepository(ICinemaDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<SeatDto>> GetAllAsync()
        {
            var seats = await _context.Seat.ToListAsync();
            return seats.Select(s => new SeatDto
            {
                Id = s.Id,
                RowNumber = s.RowNumber,
                SeatNumber = s.SeatNumber,
                Type = s.Type.ToString(),
                AuditoriumId = s.AuditoriumId
            });
        }

        public async Task<SeatDto?> GetByIdAsync(Guid id)
        {
            var seat = await _context.Seat.FirstOrDefaultAsync(s => s.Id == id);
            return seat == null
                ? null
                : new SeatDto
                {
                    Id = seat.Id,
                    RowNumber = seat.RowNumber,
                    SeatNumber = seat.SeatNumber,
                    Type = seat.Type.ToString(),
                    AuditoriumId = seat.AuditoriumId
                };
        }

        public async Task CreateAsync(CreateSeatDto dto)
        {
            if (dto.RowsCount <= 0 || dto.SeatsCount <= 0)
                throw new ArgumentException("Количество рядов и мест должно быть больше 0");

            var auditorium = await _context.Auditorium
                .FirstOrDefaultAsync(a => a.Id == dto.AuditoriumId);

            if (auditorium == null)
                throw new ArgumentException("Зал не найден");

            var seats = new List<SeatEntity>();

            for (int row = 1; row <= dto.RowsCount; row++)
            {
                for (int seatNumber = 1; seatNumber <= dto.SeatsCount; seatNumber++)
                {
                    seats.Add(new SeatEntity(row, seatNumber, SeatType.Regular, dto.AuditoriumId));
                }
            }

            await _context.Seat.AddRangeAsync(seats);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateSeatDto dto)
        {
            var seat = await _context.Seat.FirstOrDefaultAsync(s => s.Id == id);
            if (seat == null) return false;

            var auditorium = await _context.Auditorium.FirstOrDefaultAsync(a => a.Id == dto.AuditoriumId);
            if (auditorium == null)
                throw new ArgumentException("Зал не найден");

            seat.Update(dto.RowNumber, dto.SeatNumber, dto.Type, auditorium.Id);

            _context.Seat.Update(seat);
            await _context.SaveChangesAsync(CancellationToken.None);

            return true;
        }

        public async Task<bool> DeleteAsync(Guid auditoriumId)
        {
            var seat = await _context.Seat.Where(s => s.AuditoriumId == auditoriumId).ToListAsync();
            if (seat.Count == 0) return false;

            _context.Seat.RemoveRange(seat);
            await _context.SaveChangesAsync(CancellationToken.None);

            return true;
        }

        public async Task<IEnumerable<SeatDto>> GetByAuditoriumIdAsync(Guid auditoriumId)
        {
            var seats = await _context.Seat.Where(s => s.AuditoriumId == auditoriumId).ToListAsync();
            return seats.Select(s => new SeatDto
            {
                Id = s.Id,
                RowNumber = s.RowNumber,
                SeatNumber = s.SeatNumber,
                Type = s.Type.ToString(),
                AuditoriumId = s.AuditoriumId
            });
        }
    }
}