namespace Cinema.Client.DTO.Seat
{
    internal record UpdateSeatDTO
    {
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        public SeatType Type { get; set; }
        public Guid AuditoriumId { get; set; }
    };
}