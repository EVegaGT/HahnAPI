using Domain.Models.Dtos;

namespace Domain.Helpers
{
    public interface IJwtHelper
    {
        string GenerateToken(UserDto user);
        string GeneratePublicToken();
    }
}