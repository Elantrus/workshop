using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Users.Application.Services;
using Users.Core.Services;

namespace Workshop.API.IoC;

public static class AuthModule
{
    public static void AddAuth(this IServiceCollection services, IWebHostEnvironment environment)
    {
        string? environmentJwtPassword = null;
        if (!environment.IsDevelopment())
        {
            environmentJwtPassword = Environment.GetEnvironmentVariable("jwt_security") ??
                                     throw new Exception(
                                         "Para produção a variável jwt_security deve ser definida.");
        }
        else
        {
            environmentJwtPassword = "E)H@McQfTjWnZr4u7x!A%D*G-JaNdRgU";
        }
        services.AddSingleton<ITokenService>(provider => new TokenService(environmentJwtPassword));
        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(environmentJwtPassword)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
    }
}