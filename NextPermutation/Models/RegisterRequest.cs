using System.ComponentModel.DataAnnotations;

namespace NextPermutation.Models
{
    public class RegisterRequest
    {
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public String Username { get; set; }
        
        [Required] 
        public string Password { get; set; }
        
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
