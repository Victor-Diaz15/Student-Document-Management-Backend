using MediatR;
using StudentDocumentManagement.Core.Domain.Abstractions;
using StudentDocumentManagement.Core.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace StudentDocumentManagement.Core.Application.StudentFiles.Events.ChangedFileStatus;

internal class FileStatusChangedEventHandler : INotificationHandler<FileStatusChangedEvent>
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IStudentFileRepository _studentFileRepository;
    private readonly IUnitOfWork _unitOfWork;

    public FileStatusChangedEventHandler(IApplicationRepository applicationRepository, IUnitOfWork unitOfWork, IStudentFileRepository studentFileRepository)
    {
        _applicationRepository = applicationRepository;
        _unitOfWork = unitOfWork;
        _studentFileRepository = studentFileRepository;
    }

    public async Task Handle(FileStatusChangedEvent notification, CancellationToken cancellationToken)
    {
        var application = notification.Application;

        if(application != null)
        {
            var files = await _studentFileRepository.GetAllStudentFilesByApplicationIdAsync(application.Id);

            var fileInNuevo = files.Any(x => x!.Status == FileStatus.Nuevo);
            var fileInDevuelto = files.Any(x => x!.Status == FileStatus.Devuelto);
            var fileInCorregido = files.Any(x => x!.Status == FileStatus.Corregido);
            
            if(!fileInNuevo && !fileInCorregido && !fileInDevuelto)
            {
                //aplicar status de validado al application
                application.Status = ApplicationStatus.Validada;
                _applicationRepository.UpdateEntity(application);
                await _unitOfWork.SaveChangesAsync();
                return;
            }
            else if(fileInDevuelto)
            {
                //aplicar status de devulta al application
                application.Status = ApplicationStatus.Devuelta;
                _applicationRepository.UpdateEntity(application);
                await _unitOfWork.SaveChangesAsync();
                return;
            }
            else if(fileInCorregido)
            {
                //aplicar status de corregido al application
                application.Status = ApplicationStatus.Corregida;
                _applicationRepository.UpdateEntity(application);
                await _unitOfWork.SaveChangesAsync();
                return;
            }
        }
    }
}
