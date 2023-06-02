namespace Domain.Common.Configurations
{
    public class JwtBearerConfigs
    {
        public const string Key = "JwtSettings";
        public string JWT_SECRET_KEY { get; set; } = default!;
        public string JWT_AUDIENCE_TOKEN { get; set; } = default!;
        public string JWT_ISSUER_TOKEN { get; set; } = default!;
        public int JWT_EXPIRE_MINUTES { get; set; } = default;
        public int JWT_USER_EXPIRE_MINUTES { get; set; } = default;
    }
}
