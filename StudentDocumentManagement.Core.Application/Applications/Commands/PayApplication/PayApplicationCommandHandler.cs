using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Application.Shared.Results;
using StudentDocumentManagement.Core.Domain.Abstractions;
using StudentDocumentManagement.Core.Domain.Enums;

namespace StudentDocumentManagement.Core.Application.Applications.Commands.PayApplication;

internal class PayApplicationCommandHandler : ICommandHandler<PayApplicationCommand>
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PayApplicationCommandHandler(IApplicationRepository applicationRepository, IUnitOfWork unitOfWork)
    {
        _applicationRepository = applicationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(PayApplicationCommand request, CancellationToken cancellationToken)
    {
        var application = await _applicationRepository.GetByIdAsync(request.ApplicationId);

        if (application == null)
        {
            return new Result(false, $"there is not application in the system with id: {request.ApplicationId}");
        }

        application.Status = ApplicationStatus.Pagada;

        _applicationRepository.UpdateEntity(application);

        await _unitOfWork.SaveChangesAsync();

        return new Result(true, "The application status was setting to paid");
    }
}
