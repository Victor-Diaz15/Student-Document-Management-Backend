using AutoMapper;
using Microsoft.AspNetCore.Http;
using StudentDocumentManagement.Core.Application.Dtos.Accounts;
using StudentDocumentManagement.Core.Application.Dtos.Applications;
using StudentDocumentManagement.Core.Application.Interfaces;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Application.Shared.Results;
using StudentDocumentManagement.Core.Domain.Abstractions;
using System.Globalization;

namespace StudentDocumentManagement.Core.Application.Applications.Queries.GetApplicationById;

internal class GetApplicationByIdQueryHandler : IQueryHandler<GetApplicationByIdQuery, ApplicationDto>
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;

    public GetApplicationByIdQueryHandler(IApplicationRepository applicationRepository, 
        IAccountService accountService, IMapper mapper)
    {
        _applicationRepository = applicationRepository;
        _accountService = accountService;
        _mapper = mapper;
    }

    public async Task<ResultT<ApplicationDto>> Handle(GetApplicationByIdQuery request, CancellationToken cancellationToken)
    {
        var application = await _applicationRepository.GetByIdWithIncludeAndThenInclude(request.ApplicationId);

        if (application != null)
        {
            ApplicationDto applicationDto = _mapper.Map<ApplicationDto>(application);

            ResultT<StudentDto> studentDto = new(new());
            studentDto = await _accountService.GetStudentById(application.StudentId.ToString());

            applicationDto.StudentName = studentDto.Data!.FirstName + " " + studentDto.Data.LastName;
            applicationDto.StudentIdentification = studentDto.Data!.StudentId;
            

            return new ResultT<ApplicationDto>(true, "Retrieving application.", applicationDto);
        }

        return new ResultT<ApplicationDto>(false, $"There is not application in the system with this id: { request.ApplicationId }", null!);
    }
}
