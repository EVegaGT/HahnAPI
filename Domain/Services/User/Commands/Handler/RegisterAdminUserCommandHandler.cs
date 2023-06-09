﻿using AutoMapper;
using Domain.Helpers;
using Domain.Models.Responses.User;
using Infrastructure.Repositories.ReadOnlyRepositories;
using Infrastructure.Repositories;
using MediatR;
using Domain.Common.Exceptions;
using Domain.Models.Dtos;

namespace Domain.Services.User.Commands.Handler
{
    public class RegisterAdminUserCommandHandler : IRequestHandler<RegisterAdminUserCommand, AuthenticateResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IRoleReadOnlyRepository _roleReadOnlyRepository;
        private readonly IMapper _mapper;
        private readonly IJwtHelper _jwtHelper;
       
        public RegisterAdminUserCommandHandler(IUserRepository userRepository, IUserReadOnlyRepository userReadOnlyRepository, IRoleReadOnlyRepository roleReadOnlyRepository, IMapper mapper, IJwtHelper jwtHelper)
        {
            _userRepository = userRepository;
            _roleReadOnlyRepository = roleReadOnlyRepository;
            _userReadOnlyRepository = userReadOnlyRepository;
            _mapper = mapper;
            _jwtHelper = jwtHelper;
        }

        public async Task<AuthenticateResponse> Handle(RegisterAdminUserCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new HahnApiException(ErrorCodeEnum.UserRequestIsRequired);

            try
            {
                if (await _userReadOnlyRepository.ExistEmailUser(request.Email)) throw new HahnApiException(ErrorCodeEnum.EmailAlreadyExist);
                if (request.UserId == Guid.Empty) request.UserId = Guid.NewGuid();

                var role = await _roleReadOnlyRepository.GetRoleByName("Admin") ?? throw new HahnApiException(ErrorCodeEnum.UserRoleNotFound);
                AuthHelper.EncryptPassword(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
                var user = _mapper.Map<UserDto>(request);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.Active = true;
                user.RoleId = role.RoleId;

                await _userRepository.CreateUser(_mapper.Map<Infrastructure.Models.User>(user));

                //assign role to generate token as admin
                var rolDto = _mapper.Map<RoleDto>(role);
                user.Role = rolDto;

                var accessToken = _jwtHelper.GenerateToken(user);
                var response = _mapper.Map<AuthenticateResponse>(user);
                response.Token = accessToken;

                return response;
            }
            catch (HahnApiException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HahnApiException(ErrorCodeEnum.InvalidRequest, $"Could not create user. {ex.Message}");
            }
        }
    }
}
