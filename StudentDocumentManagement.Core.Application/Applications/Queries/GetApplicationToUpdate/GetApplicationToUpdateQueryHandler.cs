using AutoMapper;
using StudentDocumentManagement.Core.Application.Applications.Queries.GetApplicationById;
using StudentDocumentManagement.Core.Application.Dtos.Accounts;
using StudentDocumentManagement.Core.Application.Dtos.Applications;
using StudentDocumentManagement.Core.Application.Interfaces;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Application.Shared.Results;
using StudentDocumentManagement.Core.Domain.Abstractions;

namespace StudentDocumentManagement.Core.Application.Applications.Queries.GetApplicationToUpdate;

internal class GetApplicationToUpdateQueryHandler : IQueryHandler<GetApplicationToUpdateQuery, ApplicationDto>
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;

    public GetApplicationToUpdateQueryHandler(IApplicationRepository applicationRepository,
        IAccountService accountService, IMapper mapper)
    {
        _applicationRepository = applicationRepository;
        _accountService = accountService;
        _mapper = mapper;
    }

    public async Task<ResultT<ApplicationDto>> Handle(GetApplicationToUpdateQuery request, CancellationToken cancellationToken)
    {
        var application = await _applicationRepository.GetApplicationToUpdate(request.ApplicationId);

        if (application != null)
        {
            ApplicationDto applicationDto = _mapper.Map<ApplicationDto>(application);

            ResultT<StudentDto> studentDto = new(new());
            studentDto = await _accountService.GetStudentById(application.StudentId.ToString());

            applicationDto.StudentName = studentDto.Data!.FirstName + " " + studentDto.Data.LastName;
            applicationDto.StudentIdentification = studentDto.Data!.StudentId;


            return new ResultT<ApplicationDto>(true, "Retrieving application To Update.", applicationDto);
        }

        return new ResultT<ApplicationDto>(false, $"There is not application in the system with this id: {request.ApplicationId}", null!);
    }
}
