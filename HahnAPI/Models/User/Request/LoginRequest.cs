namespace HahnAPI.Models.User.Request
{
    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }
}
