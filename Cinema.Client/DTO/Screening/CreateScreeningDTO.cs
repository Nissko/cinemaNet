using System.ComponentModel.DataAnnotations;

namespace Cinema.Client.DTO.Screening
{
    internal record CreateScreeningDTO
    {
        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public Guid MovieId { get; set; }

        [Required]
        public Guid AuditoriumId { get; set; }
    
        [Required]
        [Range(100, 1000, ErrorMessage = "Цена должна быть в диапазоне от {1} до {2}")]
        public decimal Price { get; set; }
    }
}