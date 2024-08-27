using StudentDocumentManagement.Core.Application.Dtos.Accounts;
using StudentDocumentManagement.Core.Application.Interfaces;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Application.Shared.Results;

namespace StudentDocumentManagement.Core.Application.Users.Commands.RegisterUser;

internal class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly IAccountService _accountService;
    private readonly IStorageFile _storageFile;
    private readonly string contenedor = "users";

    public RegisterUserCommandHandler(IAccountService accountService, IStorageFile storageFile)
    {
        _accountService = accountService;
        _storageFile = storageFile;
    }

    public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var emailExisted = await _accountService.IsEmailUniqueAsync(request.Email);
        if (emailExisted)
        {
            return new Result(false, $"Email '{request.Email}' already registered.");
        }

        var userExisted = await _accountService.IsUserNameUniqueAsync(request.UserName);
        if (userExisted)
        {
            return new Result(false, $"Username '{request.UserName}' already registered.");
        }

        var identityCardExisted = await _accountService.IsIdentityCardUniqueAsync(request.IdentityCard);
        if(identityCardExisted)
        {
            return new Result(false, $"IdentityCard '{request.IdentityCard}' already registered.");
        }


        //Logica de cargar la foto de perfil del usuario
        string profilePictureUrl = "";

        if (request.ProfilePicture != null) 
        {
            using (var memoryStream = new MemoryStream())
            {
                await request.ProfilePicture.CopyToAsync(memoryStream, cancellationToken);
                var contenido = memoryStream.ToArray();
                var extension = Path.GetExtension(request.ProfilePicture.FileName);

                profilePictureUrl = await _storageFile
                    .SaveFile(contenido, extension, contenedor, request.ProfilePicture.ContentType);
            }
        }

        var userDto = new RegisterUserRequestDto()
        {
            IdentityCard = request.IdentityCard,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.UserName,
            Password = request.Password,
            PhoneNumber = request.PhoneNumber,
            Rol = request.Rol,
            ProfilePicture = profilePictureUrl,
        };

        var result = await _accountService.RegisterUserAsync(userDto);

        return result;
    }
}
