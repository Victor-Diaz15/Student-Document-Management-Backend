using MediatR;
using StudentDocumentManagement.Core.Application.Applications.Events.ApplicationCompleted;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Application.Shared.Results;
using StudentDocumentManagement.Core.Domain.Abstractions;

namespace StudentDocumentManagement.Core.Application.Applications.Commands.CompleteApplication;

internal class CompleteApplicationCommandHandler : ICommandHandler<CompleteApplicationCommand, string>
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IMediator _mediator;

    public CompleteApplicationCommandHandler(IApplicationRepository applicationRepository, IMediator mediator)
    {
        _applicationRepository = applicationRepository;
        _mediator = mediator;
    }

    public async Task<ResultT<string>> Handle(CompleteApplicationCommand request, CancellationToken cancellationToken)
    {
        var applicationNumberId = await _applicationRepository.CompleteApplication(request.ApplicationIds);

        var notification = new ApplicationCompletedEvent(request.ApplicationIds, applicationNumberId);
        await _mediator.Publish(notification, cancellationToken);

        return new ResultT<string>(true, "Applications Completed", applicationNumberId);
    }
}
