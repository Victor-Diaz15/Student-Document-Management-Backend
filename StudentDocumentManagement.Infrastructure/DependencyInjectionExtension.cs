﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentDocumentManagement.Core.Application.Interfaces;
using StudentDocumentManagement.Core.Domain.Settings;
using StudentDocumentManagement.Infrastructure.Services;
using System.Reflection;

namespace StudentDocumentManagement.Infrastructure;

//Extension Methods - application of this design pattern Decorator
public static class DependencyInjectionExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {

        services.Configure<MailSettings>(config.GetSection("MailSettings"));

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddHttpContextAccessor();

        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IStorageFile, StorageFileLocalService>();

        return services;
    }
}
