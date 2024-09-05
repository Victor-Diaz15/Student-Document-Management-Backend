using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentDocumentManagement.Core.Domain.Abstractions;
using StudentDocumentManagement.Infrastructure.Persistence.Contexts;
using StudentDocumentManagement.Infrastructure.Persistence.Repositories;

namespace StudentDocumentManagement.Infrastructure.Persistence;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddInfrastructurePersistence(this IServiceCollection services, IConfiguration config)
    {
        //agregando el contexto de base de datos y la cadena de conexión
        services.AddDbContext<MainDbContext>(opts =>
            opts.UseSqlServer(config.GetConnectionString("defaultConnection"),
                m => m.MigrationsAssembly(typeof(MainDbContext).Assembly.FullName)
            ));

        //Repositories
        services.AddScoped(typeof(IGenericBaseRepository<>), typeof(GenericBaseRepository<>));
        services.AddScoped<IStudentFileRepository, StudentFileRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
