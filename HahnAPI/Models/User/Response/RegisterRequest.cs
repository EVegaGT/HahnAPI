namespace HahnAPI.Models.User.Response
{
    public class RegisterRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }
}
