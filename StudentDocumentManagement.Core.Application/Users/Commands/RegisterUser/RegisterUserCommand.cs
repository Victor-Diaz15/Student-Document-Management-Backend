using Microsoft.AspNetCore.Http;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using System.ComponentModel.DataAnnotations;

namespace StudentDocumentManagement.Core.Application.Users.Commands.RegisterUser;

public sealed record RegisterUserCommand(
    string IdentityCard,
    string FirstName,
    string LastName,
    string Email,
    string UserName,
    string Password,
    string PhoneNumber,
    int Rol,
    [DataType(DataType.Upload)]
    IFormFile? ProfilePicture
    ) : ICommand;
