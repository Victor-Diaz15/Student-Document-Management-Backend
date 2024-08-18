using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentDocumentManagement.Core.Application.Interfaces;
using StudentDocumentManagement.Core.Domain.Settings;
using StudentDocumentManagement.Infrastructure.Services;

namespace StudentDocumentManagement.Infrastructure;

//Extension Methods - application of this design pattern Decorator
public static class DependencyInjectionExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {

        services.Configure<MailSettings>(config.GetSection("MailSettings"));
        services.AddTransient<IEmailService, EmailService>();

        return services;
    }
}
