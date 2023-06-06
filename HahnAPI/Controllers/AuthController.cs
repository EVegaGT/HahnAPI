using AutoMapper;
using Domain.Common.Exceptions;
using Domain.Helpers;
using Domain.Models.Requests.User;
using Domain.Models.Responses.User;
using Domain.Services.User.Commands;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace HahnAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IJwtHelper _jwtHelper;

        public AuthController(IMediator mediator, IMapper mapper, IJwtHelper jwtHelper)
        {
            _mediator = mediator;
            _mapper = mapper;
            _jwtHelper = jwtHelper;
        }

        /// <summary>
        /// Get an anonymous token.
        /// </summary>
        /// <remarks>
        /// Sample request: Request data from authentication token
        /// 
        ///     GET / Todo
        ///     
        ///     /api/Auth
        ///
        /// </remarks>
        /// <returns>A jwt token </returns>
        /// <response code="200">Returns a jwt anonymous token</response>
        /// <response code="404">If the item doesn't exist</response>
        [HttpGet("GetPublicToken", Name = "GetToken")]
        [SwaggerOperation(Summary = "Get token data", Description = "Request data from authentication token")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Auth()
        {
            try
            {
                var token = _jwtHelper.GeneratePublicToken();
                return Ok(token);
            }
            catch (HahnApiException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Errors);
            }
            catch (Exception ex)
            {
                var errors = new List<Error>
                {
                    new Error(ErrorCodeEnum.BadRequest, ex.GetExceptionMessage())
                };

                return StatusCode((int)HttpStatusCode.BadRequest, errors);
            }
        }

        /// <summary>
        /// Create a new User.
        /// </summary>
        /// <remarks>
        /// Sample request: Request data to create a User
        /// 
        ///     POST / Todo
        ///     
        ///     {
        ///       "name": "John",
        ///       "lastName": "Smith",
        ///       "email": "test@gmail.com",
        ///       "password": "test",
        ///       "facebookId": "123456789101112",
        ///       "googleId": "123456789101112"
        ///     }
        ///
        /// </remarks>
        /// <returns>The UserId from the newly contact </returns>
        /// <response code="201">Returns the contact UserId</response>
        /// <response code="400">If an error occurs</response>
        /// <response code="401">If it is unauthorized</response>
        [HttpPost("Register", Name = "CreateUser")]
        [Authorize]
        [SwaggerOperation(Summary = "Create a User", Description = "Create a new user")]
        [ProducesResponseType(typeof(AuthenticateResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }

                var command = _mapper.Map<RegisterAdminUserCommand>(user);
                var result = await _mediator.Send(command);
                return Created("api/Register", result);
            }
            catch (HahnApiException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Login User.
        /// </summary>
        /// <remarks>
        /// Sample request: Login a User
        /// 
        ///     Post /api/Auth/Login
        ///     {
        ///         "email" : "test@test.com",
        ///         "password": "pasword123"
        ///     }
        ///
        /// </remarks>
        /// <returns>Login success response </returns>
        /// <response code="200">Returns a jwt anonymous token</response>
        /// <response code="404">If the user doesn't exist</response>
        [HttpPost("Login", Name = "Login")]
        [Authorize]
        [SwaggerOperation(Summary = "Login", Description = "Login user and return authentication token")]
        [ProducesResponseType(typeof(AuthenticateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }

                var command = _mapper.Map<LoginUserCommand>(login);
                var result = await Task.Run(() => _mediator.Send(command));
                return Ok(result);
            }
            catch (HahnApiException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Errors);
            }
            catch (Exception ex)
            {
                var errors = new List<Error>
                {
                    new Error(ErrorCodeEnum.BadRequest, ex.GetExceptionMessage())
                };

                return StatusCode((int)HttpStatusCode.BadRequest, errors);
            }
        }
    }
}
