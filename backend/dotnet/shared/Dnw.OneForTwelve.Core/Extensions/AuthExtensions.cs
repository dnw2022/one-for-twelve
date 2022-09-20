using Dnw.OneForTwelve.Core.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Dnw.OneForTwelve.Core.Extensions;

public static class AuthExtensions
{
    public static void AddFirebaseAuth(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = JwtBearerConfig.Authority;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = JwtBearerConfig.Authority,
                    ValidateAudience = true,
                    ValidAudience = JwtBearerConfig.Audience,
                    ValidateLifetime = true
                };
            });
        
        services.AddAuthorization(o => o.AddPolicy("NotAnonymous", b => { b.RequireAuthenticatedUser(); }));
    }
}