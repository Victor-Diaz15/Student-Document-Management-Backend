using StudentDocumentManagement.Core.Application.Interfaces;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Application.Shared.Results;
using StudentDocumentManagement.Core.Domain.Abstractions;

namespace StudentDocumentManagement.Core.Application.StudentFiles.Commands.ChangeStatus;

internal class ChangeStatusStudentFileCommandHandler : ICommandHandler<ChangeStatusStudentFileCommand>
{
    private readonly IStudentFileRepository _studentFileRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeStatusStudentFileCommandHandler(IStudentFileRepository studentFileRepository, IUnitOfWork unitOfWork)
    {
        _studentFileRepository = studentFileRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ChangeStatusStudentFileCommand request, CancellationToken cancellationToken)
    {
        var fileEntity = await _studentFileRepository.GetByIdAsync(request.FileId);

        if (fileEntity == null)
            return new Result(false, $"No existe archivo con el id: {request.FileId}");

        fileEntity.Status = request.Status;

        //guardando el registro
        _studentFileRepository.UpdateEntity(fileEntity);

        //persistir los cambios en la db
        await _unitOfWork.SaveChangesAsync();

        return new Result(true, "File Updated");
    }
}
