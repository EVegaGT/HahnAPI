using Domain.Models.Responses.User;
using MediatR;

namespace Domain.Services.User.Commands
{
    public class LoginUserCommand : IRequest<AuthenticateResponse>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}