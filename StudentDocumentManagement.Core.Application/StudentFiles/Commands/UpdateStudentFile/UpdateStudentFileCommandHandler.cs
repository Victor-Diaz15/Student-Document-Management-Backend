using MediatR;
using Microsoft.AspNetCore.Http;
using StudentDocumentManagement.Core.Application.Interfaces;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Application.Shared.Results;
using StudentDocumentManagement.Core.Application.StudentFiles.Events.ChangedFileStatus;
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
    private readonly IMediator _mediator;
    private string contenedor = "studentfiles";

    public UpdateStudentFileCommandHandler(IStorageFile storageFile,
        IStudentFileRepository studentFileRepository, IUnitOfWork unitOfWork, IMediator mediator)
    {
        _storageFile = storageFile;
        _studentFileRepository = studentFileRepository;
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task<Result> Handle(UpdateStudentFileCommand request, CancellationToken cancellationToken)
    {

        var fileEntity = await _studentFileRepository.GetByIdWithIncludeAndThenInclude(request.FileId);

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
        fileEntity.Status = (fileEntity.Status == FileStatus.Devuelto) ? FileStatus.Corregido : fileEntity.Status;

        //guardando el registro
        _studentFileRepository.UpdateEntity(fileEntity);

        //persistir los cambios en la db
        await _unitOfWork.SaveChangesAsync();

        var notification = new FileStatusChangedEvent(fileEntity.ApplicationsFiles![0].Application!);
        await _mediator.Publish(notification, cancellationToken);

        return new Result(true, "File Updated");
    }
}
