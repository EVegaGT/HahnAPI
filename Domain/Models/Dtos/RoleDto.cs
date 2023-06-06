namespace Domain.Models.Dtos
{
    public class RoleDto
    {
        public Guid RoleId { get; set; }
        public string? Name { get; set; }
        public bool IsDeleted { get; set; }
        public List<UserDto>? Users { get; set; }
    }
}