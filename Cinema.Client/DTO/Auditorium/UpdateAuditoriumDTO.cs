using System.ComponentModel.DataAnnotations;

namespace Cinema.Client.DTO.Auditorium
{
    internal record UpdateAuditoriumDTO
    {
        [Required]
        public int Number { get; set; }

        [Required]
        public int RowsCount { get; set; }

        [Required]
        public int SeatsPerRow { get; set; }

        [Required]
        public Guid CinemaId { get; set; }
    };
}