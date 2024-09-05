using Microsoft.AspNetCore.Http;
using StudentDocumentManagement.Core.Application.Interfaces;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Application.Shared.Results;
using StudentDocumentManagement.Core.Domain.Abstractions;
using StudentDocumentManagement.Core.Domain.Entities;
using StudentDocumentManagement.Core.Domain.Enums;
using System.Diagnostics;

namespace StudentDocumentManagement.Core.Application.StudentFiles.Commands.UpdateStudentFile;

internal class UpdateStudentFileCommandHandler : ICommandHandler<UpdateStudentFileCommand>
{
    private readonly IStorageFile _storageFile;
    private readonly IStudentFileRepository _studentFileRepository;
    private readonly IUnitOfWork _unitOfWork;
    private string contenedor = "studentfiles";

    public UpdateStudentFileCommandHandler(IStorageFile storageFile,
        IStudentFileRepository studentFileRepository, IUnitOfWork unitOfWork)
    {
        _storageFile = storageFile;
        _studentFileRepository = studentFileRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateStudentFileCommand request, CancellationToken cancellationToken)
    {

        var fileEntity = await _studentFileRepository.GetByIdAsync(request.FileId);

        if (fileEntity == null)
            return new Result(false, $"No existe archivo con el id: {request.FileId}");

        //Aqui va la logica de guardar un archivo.
        string fileUrl;

        using (var memoryStream = new MemoryStream())
        {
            await request.File.CopyToAsync(memoryStream, cancellationToken);
            var contenido = memoryStream.ToArray();
            var extension = Path.GetExtension(request.File.FileName);
            contenedor += $"/{fileEntity.StudentId}";

            fileUrl = await _storageFile
                .UpdateFile(contenido, extension, contenedor, request.File.ContentType, fileEntity.Url);
        }

        fileEntity.Url = fileUrl;
        fileEntity.FileType = request.FileType;

        //guardando el registro
        _studentFileRepository.UpdateEntity(fileEntity);

        //persistir los cambios en la db
        await _unitOfWork.SaveChangesAsync();

        return new Result(true, "File Updated");
    }
}
