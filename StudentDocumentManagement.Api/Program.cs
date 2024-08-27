using Microsoft.AspNetCore.Identity;
using StudentDocumentManagement.Infrastructure.Identity.Context;
using StudentDocumentManagement.Infrastructure.Identity.Entities;
using StudentDocumentManagement.Infrastructure.Identity;
using StudentDocumentManagement.Core.Application;
using StudentDocumentManagement.Infrastructure;
using StudentDocumentManagement.Api.Middlewares;
using Serilog;
using StudentDocumentManagement.Api.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configurando Identity para el manejo de usuarios y roles de nuestro sistema
builder.Services.AddIdentity<UserApp, IdentityRole>()
    .AddEntityFrameworkStores<IdentityContext>()
    .AddDefaultTokenProviders();


builder.Services.AddScoped(typeof(CommandValidatorFilter<,,>));

//Agregando la capa de application
builder.Services.AddApplicationLayer(builder.Configuration);

//Agregando la capa de infrastructure
builder.Services.AddInfrastructureServices(builder.Configuration);

//Agregando la capa de identity
builder.Services.AddIdentityInfrastructure(builder.Configuration);

//Registrando Serilog y su configuracion
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));


builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(opt => 
    opt.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
