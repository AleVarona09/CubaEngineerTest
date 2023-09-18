namespace NextPermutation.Models
{
    public class AuthenticationConfiguration
    {
        public string AccesTokenSecretKey { get; set; }
        public int AccesTokenExpMin { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string RefreshTokenSecretKey { get; set; }
        public double RefreshTokenExpMin { get; set; }
    }
}