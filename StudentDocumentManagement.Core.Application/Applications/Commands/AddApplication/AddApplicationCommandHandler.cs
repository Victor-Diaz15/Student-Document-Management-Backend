using AutoMapper;
using Microsoft.AspNetCore.Http;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Application.Shared.Results;
using StudentDocumentManagement.Core.Domain.Abstractions;
using StudentDocumentManagement.Core.Domain.Entities;

namespace StudentDocumentManagement.Core.Application.Applications.Commands.AddApplication;

internal class AddApplicationCommandHandler : ICommandHandler<AddApplicationCommand>
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IMapper _mapper;
    public AddApplicationCommandHandler(IApplicationRepository applicationRepository, 
        IHttpContextAccessor contextAccessor, IMapper mapper, 
        IUnitOfWork unitOfWork)
    {
        _applicationRepository = applicationRepository;
        _contextAccessor = contextAccessor;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AddApplicationCommand request, CancellationToken cancellationToken)
    {
        var claims = _contextAccessor.HttpContext?.User.Claims;
        var userIdClaim = claims?.FirstOrDefault(c => c.Type == "uid")?.Value;

        var entity = new Domain.Entities.Application()
        {
            StudentId = new Guid(userIdClaim!),
            ServiceId = request.ServiceId,
            Status = Domain.Enums.ApplicationStatus.Nueva,
            Files = _mapper.Map<List<ApplicationStudentFile>>(request.Files)
        };

        await _applicationRepository.AddEntityAsync(entity);

        await _unitOfWork.SaveChangesAsync();

        return new Result(true, "Application Saved");
    }
}
