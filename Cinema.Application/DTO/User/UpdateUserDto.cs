using System.ComponentModel.DataAnnotations;

namespace Cinema.Application.DTO.User
{
    public record UpdateUserDto
    {
        [StringLength(100)]
        public string Name { get; set; }
    
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
    
        public string Password { get; set; }
    };
}