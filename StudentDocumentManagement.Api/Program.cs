using Microsoft.AspNetCore.Identity;
using StudentDocumentManagement.Infrastructure.Identity.Context;
using StudentDocumentManagement.Infrastructure.Identity.Entities;
using StudentDocumentManagement.Infrastructure.Identity;
using StudentDocumentManagement.Core.Application;
using StudentDocumentManagement.Infrastructure;

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

//Agregando la capa de application
builder.Services.AddApplicationLayer();

//Agregando la capa de infrastructure
builder.Services.AddInfrastructureServices(builder.Configuration);

//Agregando la capa de identity
builder.Services.AddIdentityInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
