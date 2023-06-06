using AutoMapper;
using Domain.Common.Exceptions;
using Domain.Helpers;
using Domain.Models.Dtos;
using Domain.Models.Responses.User;
using Infrastructure.Repositories.ReadOnlyRepositories;
using MediatR;

namespace Domain.Services.User.Commands.Handler
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthenticateResponse>
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IJwtHelper _jwtHelper;
        private readonly IMapper _mapper;

        public LoginUserCommandHandler(IUserReadOnlyRepository userReadOnlyRepository, IJwtHelper jwtHelper, IMapper mapper)
        {
            _userReadOnlyRepository = userReadOnlyRepository;
            _jwtHelper = jwtHelper;
            _mapper = mapper;
        }

        public async Task<AuthenticateResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new HahnApiException(ErrorCodeEnum.LoginUserRequestIsRequired);

            try
            {
                var user = _mapper.Map<UserDto>(await _userReadOnlyRepository.GetUserByEmail(request.Email));

                if (user == null || string.IsNullOrEmpty(user.Email)) throw new HahnApiException(ErrorCodeEnum.UserNotFound);

                if (!AuthHelper.VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt)) throw new HahnApiException(ErrorCodeEnum.LoginFailed);

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
                throw new HahnApiException(ErrorCodeEnum.InvalidRequest, $"Could not login user. {ex.Message}");
            }
        }
    }
}
