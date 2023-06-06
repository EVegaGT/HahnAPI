namespace Domain.Models.Dtos
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public Guid RoleId { get; set; }

        public RoleDto? Role { get; set; }
    }
}
