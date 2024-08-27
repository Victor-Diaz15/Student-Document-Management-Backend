using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentDocumentManagement.Core.Domain.Settings;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace StudentDocumentManagement.Core.Application;

//Extension Methods - application of this design pattern Decorator
public static class DependencyInjectionExtension
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        //Agregando mediatR
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.Configure<FileUploadSettings>(configuration.GetSection("FileUploadSettings"));

        //Fluent Validation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
