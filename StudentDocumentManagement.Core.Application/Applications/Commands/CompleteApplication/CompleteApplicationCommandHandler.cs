using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Application.Shared.Results;
using StudentDocumentManagement.Core.Domain.Abstractions;

namespace StudentDocumentManagement.Core.Application.Applications.Commands.CompleteApplication;

internal class CompleteApplicationCommandHandler : ICommandHandler<CompleteApplicationCommand, string>
{
    private readonly IApplicationRepository _applicationRepository;

    public CompleteApplicationCommandHandler(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }

    public async Task<ResultT<string>> Handle(CompleteApplicationCommand request, CancellationToken cancellationToken)
    {
        var applicationNumberId = await _applicationRepository.CompleteApplication(request.ApplicationIds);

        return new ResultT<string>(true, applicationNumberId);
    }
}
