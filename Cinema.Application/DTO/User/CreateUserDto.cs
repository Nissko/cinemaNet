using System.ComponentModel.DataAnnotations;

namespace Cinema.Application.DTO.User
{
    public record CreateUserDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;
    
        [Required]
        public string Password { get; set; } = string.Empty;
    };
}