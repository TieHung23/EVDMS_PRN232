using System.Security.Claims;
using System.Text;
using EVDMS.BusinessLogicLayer.Configure.Option;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace EVDMS.Api.Configure;

public static class AuthenticationConfigure
{
    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
            {
                var jwt = configuration
                    .GetSection("JwtSettings")
                    .Get<JwtModel>()!;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwt.Issuer,
                    ValidAudience = jwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwt.SecretKey)
                    ),

                    RoleClaimType = ClaimTypes.Role
                };
            });


        return services;
    }
}