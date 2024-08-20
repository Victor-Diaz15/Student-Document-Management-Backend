using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace StudentDocumentManagement.Core.Application;

//Extension Methods - application of this design pattern Decorator
public static class DependencyInjectionExtension
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        //Agregando mediatR
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        //Fluent Validation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        #region Services

        #endregion

        return services;
    }
}
