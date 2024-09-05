using Microsoft.AspNetCore.Http;
using StudentDocumentManagement.Core.Application.Interfaces;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Application.Shared.Results;
using StudentDocumentManagement.Core.Domain.Abstractions;
using StudentDocumentManagement.Core.Domain.Entities;
using StudentDocumentManagement.Core.Domain.Enums;

namespace StudentDocumentManagement.Core.Application.StudentFiles.Commands.AddStudentFile;

internal class AddStudentFileCommandHandler : ICommandHandler<AddStudentFileCommand, Guid>
{
    private readonly IStorageFile _storageFile;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IStudentFileRepository _studentFileRepository;
    private readonly IUnitOfWork _unitOfWork;
    private string contenedor = "studentfiles";

    public AddStudentFileCommandHandler(IStorageFile storageFile, IHttpContextAccessor contextAccessor, IStudentFileRepository studentFileRepository, IUnitOfWork unitOfWork)
    {
        _storageFile = storageFile;
        _contextAccessor = contextAccessor;
        _studentFileRepository = studentFileRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultT<Guid>> Handle(AddStudentFileCommand request, CancellationToken cancellationToken)
    {
        var claims = _contextAccessor.HttpContext?.User.Claims;
        var userIdClaim = claims?.FirstOrDefault(c => c.Type == "uid")?.Value;

        //Aqui va la logica de guardar un archivo.
        string fileUrl;

        using (var memoryStream = new MemoryStream())
        {
            await request.File.CopyToAsync(memoryStream, cancellationToken);
            var contenido = memoryStream.ToArray();
            var extension = Path.GetExtension(request.File.FileName);
            contenedor += $"/{ userIdClaim }";

            fileUrl = await _storageFile
                .SaveFile(contenido, extension, contenedor, request.File.ContentType);
        }

        //mapear a la entidad
        StudentFile entity = new()
        {
            StudentId = new Guid(userIdClaim!),
            Url = fileUrl,
            FileType = request.FileType,
            Status = FileStatus.Nuevo
        };

        //guardando el registro
        await _studentFileRepository.AddEntityAsync(entity);

        //persistir los cambios en la db
        await _unitOfWork.SaveChangesAsync();

        return new ResultT<Guid>(true, "File Saved", entity.Id);
    }
}
