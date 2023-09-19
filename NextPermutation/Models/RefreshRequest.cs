using System.ComponentModel.DataAnnotations;

namespace NextPermutation.Models
{
    public class RefreshRequest
    {
        [Required]
        public string RefreshToken { get; set; }

    }
}
