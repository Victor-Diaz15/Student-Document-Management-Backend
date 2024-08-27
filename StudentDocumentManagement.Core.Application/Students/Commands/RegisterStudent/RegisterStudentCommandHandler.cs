using MediatR;
using StudentDocumentManagement.Core.Application.Dtos.Accounts;
using StudentDocumentManagement.Core.Application.Interfaces;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Application.Shared.Results;
using StudentDocumentManagement.Core.Application.Students.Events.StudentRegistered;

namespace StudentDocumentManagement.Core.Application.Students.Commands.RegisterStudent;

internal class RegisterStudentCommandHandler : ICommandHandler<RegisterStudentCommand>
{
    private readonly IAccountService _accountService;
    private readonly IMediator _mediator;
    private readonly IStorageFile _storageFile;
    private readonly string contenedor = "students";

    public RegisterStudentCommandHandler(IAccountService accountService, IMediator mediator, IStorageFile storageFile)
    {
        _accountService = accountService;
        _mediator = mediator;
        _storageFile = storageFile;
    }

    public async Task<Result> Handle(RegisterStudentCommand request, CancellationToken cancellationToken)
    {
        var emailExisted = await _accountService.IsEmailUniqueAsync(request.Email);
        if (emailExisted)
        {
            return new Result(false, $"Email '{request.Email}' already registered.");
        }

        var identityCardExisted = await _accountService.IsIdentityCardUniqueAsync(request.IdentityCard);
        if (identityCardExisted)
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

        var studentReqDto = new RegisterStudentRequestDto()
        {
            IdentityCard = request.IdentityCard,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = request.Password,
            PhoneNumber = request.PhoneNumber,
            ProfilePicture = profilePictureUrl
        };

        var result = await _accountService.RegisterStudentAsync(studentReqDto);

        if (result.Success) 
        {
            var notification = new StudentRegisteredEvent(request.Email, result.Data!.Username, request.Password);
            await _mediator.Publish(notification);
        }

        return result;
    }
}
