using MediatR;
using StudentDocumentManagement.Core.Application.Interfaces;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Application.Shared.Results;
using StudentDocumentManagement.Core.Application.StudentFiles.Events.ChangedFileStatus;
using StudentDocumentManagement.Core.Application.Students.Events.StudentRegistered;
using StudentDocumentManagement.Core.Domain.Abstractions;

namespace StudentDocumentManagement.Core.Application.StudentFiles.Commands.ChangeStatus;

internal class ChangeStatusStudentFileCommandHandler : ICommandHandler<ChangeStatusStudentFileCommand>
{
    private readonly IStudentFileRepository _studentFileRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    public ChangeStatusStudentFileCommandHandler(IStudentFileRepository studentFileRepository, IUnitOfWork unitOfWork, IMediator mediator)
    {
        _studentFileRepository = studentFileRepository;
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task<Result> Handle(ChangeStatusStudentFileCommand request, CancellationToken cancellationToken)
    {
        var fileEntity = await _studentFileRepository.GetByIdWithIncludeAndThenInclude(request.FileId);

        if (fileEntity == null)
            return new Result(false, $"No existe archivo con el id: {request.FileId}");

        fileEntity.Status = request.Status;

        //guardando el registro
        _studentFileRepository.UpdateEntity(fileEntity);

        //persistir los cambios en la db
        await _unitOfWork.SaveChangesAsync();

        var notification = new FileStatusChangedEvent(fileEntity.ApplicationsFiles![0].Application!);
        await _mediator.Publish(notification, cancellationToken);

        return new Result(true, "File Updated");
    }
}
