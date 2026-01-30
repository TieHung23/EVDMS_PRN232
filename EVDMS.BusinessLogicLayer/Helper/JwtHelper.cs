using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EVDMS.BusinessLogicLayer.Configure.Option;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EVDMS.BusinessLogicLayer.Helper;

public class JwtHelper : IJwtHelper
{
    private readonly JwtModel _jwtModel;

    public JwtHelper(IOptions<JwtModel> options)
    {
        _jwtModel = options.Value;
    }


    private string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString("N");
    }
    public (string, string, string) GenerateToken(string userName, string fullName, string roleName, string userId)
    {
        var issuer = _jwtModel.Issuer;
        var audience = _jwtModel.Audience;
        var secretKey = _jwtModel.SecretKey;
        var expiresMinutes = _jwtModel.AccessTokenExpirationMinutes;
        var refreshTokenExpirationDays = _jwtModel.RefreshTokenExpirationDays;

        var key = Encoding.UTF8.GetBytes(secretKey);
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(JwtRegisteredClaimNames.Jti, Math.Abs(BitConverter.ToInt64(Guid.NewGuid().ToByteArray())).ToString()),
                new Claim("userName", userName),
                new Claim("fullName", fullName),
                new Claim(ClaimTypes.Role, roleName),
                new Claim(ClaimTypes.NameIdentifier, userId)
            ]),
            Expires = DateTime.Now.AddMinutes(expiresMinutes),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenId = token.Id;

        var accessToken = tokenHandler.WriteToken(token);
        var refreshToken = GenerateRefreshToken();


        return (tokenId, accessToken, refreshToken);
    }
}