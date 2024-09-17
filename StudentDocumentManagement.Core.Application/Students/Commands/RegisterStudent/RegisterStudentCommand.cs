using Microsoft.AspNetCore.Http;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using System.ComponentModel.DataAnnotations;

namespace StudentDocumentManagement.Core.Application.Students.Commands.RegisterStudent;

public sealed record RegisterStudentCommand(
    string IdentityCard,
    string FirstName,
    string LastName,
    string Email,
    string StudentId,
    string Password,
    string PhoneNumber,
    [DataType(DataType.Upload)]
    IFormFile? ProfilePicture
    ) : ICommand;
