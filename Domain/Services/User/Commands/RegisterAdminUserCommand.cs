﻿using Domain.Models.Responses.User;
using MediatR;

namespace Domain.Services.User.Commands
{
    public class RegisterAdminUserCommand : IRequest<AuthenticateResponse>
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
