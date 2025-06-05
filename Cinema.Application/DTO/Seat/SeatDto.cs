namespace Cinema.Application.DTO.Seat
{
    public record SeatDto
    {
        public Guid Id { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        public string Type { get; set; }
        public Guid AuditoriumId { get; set; }
    };
}