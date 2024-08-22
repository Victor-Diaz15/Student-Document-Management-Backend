using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using StudentDocumentManagement.Core.Application.Interfaces;
using StudentDocumentManagement.Core.Application.Shared.Results;
using StudentDocumentManagement.Core.Domain.Settings;
using StudentDocumentManagement.Infrastructure.Identity.Context;
using StudentDocumentManagement.Infrastructure.Identity.Services;
using System.Text;

namespace StudentDocumentManagement.Infrastructure.Identity;

//Main reason for creating this class is to follow the Single responsability
public static class DependencyInjectionExtension
{
    // Extension methods | "Decorator"
    // This allows us to extend and create new functionallity following "Open-Closed Principle"
    public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services, IConfiguration config)
    {

        //Registrando el contexto de identity.
        services.AddDbContext<IdentityContext>(options =>
                    options.UseSqlServer(config.GetConnectionString("DefaultConnection"),
                    m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));


        services.Configure<JWTSettings>(config.GetSection("JWTSettings"));

        //Agregando la configuracion de JWT.
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.RequireHttpsMetadata = false;
            opt.SaveToken = false;
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = config["JWTSettings:Issuer"],
                ValidAudience = config["JWTSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWTSettings:Key"]!))
            };
            opt.Events = new JwtBearerEvents()
            {
                OnAuthenticationFailed = e =>
                {
                    e.NoResult();
                    e.Response.StatusCode = 500;
                    e.Response.ContentType = "text/plain";
                    return e.Response.WriteAsync(e.Exception.ToString());
                },
                OnChallenge = e =>
                {
                    e.HandleResponse();
                    e.Response.StatusCode = 401;
                    e.Response.ContentType = "application/json";
                    var result = JsonConvert.SerializeObject(new Result(true, "You're not authorized"));
                    return e.Response.WriteAsync(result);
                },
                OnForbidden = e =>
                {
                    e.Response.StatusCode = 403;
                    e.Response.ContentType = "application/json";
                    var result = JsonConvert.SerializeObject(new Result(true, "You're not authorized to access this resource"));
                    return e.Response.WriteAsync(result);
                }
            };
        });

        services.AddScoped<IAccountService, AccountService>();

        return services;
    }
}
