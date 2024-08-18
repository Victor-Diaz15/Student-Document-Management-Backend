using StudentDocumentManagement.Core.Application.Interfaces.Messaging;

namespace StudentDocumentManagement.Core.Application.Students.Commands.RegisterStudent;

public sealed record RegisterStudentCommand(
    string IdentityCard,
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string PhoneNumber,
    int Rol,
    string ProfilePicture
    ) : ICommand;
