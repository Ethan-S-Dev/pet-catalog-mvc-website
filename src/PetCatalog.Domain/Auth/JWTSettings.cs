namespace PetCatalog.Domain.Auth
{
    public class JWTSettings
    {
        public string SecretKey { get; set; }
        public int JwtExpiresIn { get; set; }
        public int RefreshExpiresIn { get; set; }
    }
}
