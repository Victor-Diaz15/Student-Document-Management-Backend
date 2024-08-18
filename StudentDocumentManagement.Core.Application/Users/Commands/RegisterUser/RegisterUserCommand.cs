using StudentDocumentManagement.Core.Application.Interfaces.Messaging;

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
    string ProfilePicture
    ) : ICommand;
