using System.ComponentModel.DataAnnotations;

namespace NextPermutation.Models
{
    public class LoginRequest
    {
        [Required]
        public String Username { get; set; }

        [Required]
        public string Password { get; set; }
        
    }
}
