using System.ComponentModel.DataAnnotations;

namespace Cinema.Client.DTO.Cinema
{
    internal record CreateCinemaDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;
    }
}