using Domain.Common.Configurations;
using Domain.Common.Exceptions;
using Domain.Models.Dtos;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Domain.Helpers
{
    public class JwtHelper : IJwtHelper
    {
        private readonly IOptions<JwtBearerConfigs> _options;
        private readonly string _secretKey;
        private readonly string _audienceToken;
        private readonly string _issuerToken;
        private readonly int _expireTime;

        public JwtHelper(IOptions<JwtBearerConfigs> options)
        {
            _options = options;
            _secretKey = _options.Value.JWT_SECRET_KEY;
            _audienceToken = _options.Value.JWT_AUDIENCE_TOKEN;
            _issuerToken = _options.Value.JWT_ISSUER_TOKEN;
            _expireTime = _options.Value.JWT_USER_EXPIRE_MINUTES;
        }

        public string GenerateToken(UserDto user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(_secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // create a claimsIdentity
            ClaimsIdentity claimsIdentity = new (new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Name ?? string.Empty),
                new Claim(ClaimTypes.Surname, user.LastName ?? string.Empty),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Role, user.Role == null ? "Client" : user.Role.Name ?? "Client")
            });

            // create token to the user
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: _audienceToken,
                issuer: _issuerToken,
                subject: claimsIdentity,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_expireTime),
                signingCredentials: signingCredentials) ?? throw new HahnApiException(ErrorCodeEnum.TokenNotGenerated);

            return tokenHandler.WriteToken(jwtSecurityToken);
        }

        public string GeneratePublicToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(_secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // create a claimsIdentity
            ClaimsIdentity claimsIdentity = new (new[] { new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) });

            // create public token
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: _audienceToken,
                issuer: _issuerToken,
                subject: claimsIdentity,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_expireTime),
                signingCredentials: signingCredentials) ?? throw new HahnApiException(ErrorCodeEnum.TokenNotGenerated);

            return tokenHandler.WriteToken(jwtSecurityToken);
        }
    }
}
