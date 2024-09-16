using AutoMapper;
using StudentDocumentManagement.Core.Application.Dtos.Accounts;
using StudentDocumentManagement.Core.Application.Dtos.Applications;
using StudentDocumentManagement.Core.Application.Interfaces;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Application.Shared.Results;
using StudentDocumentManagement.Core.Domain.Abstractions;

namespace StudentDocumentManagement.Core.Application.Applications.Queries.GetApplicationByFilters;

internal class GetApplicationsByFiltersQueryHandler : IQueryHandler<GetApplicationsByFiltersQuery, List<ApplicationDto>>
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;
    public GetApplicationsByFiltersQueryHandler(IApplicationRepository applicationRepository, IAccountService accountService, IMapper mapper)
    {
        _applicationRepository = applicationRepository;
        _accountService = accountService;
        _mapper = mapper;
    }

    public async Task<ResultT<List<ApplicationDto>>> Handle(GetApplicationsByFiltersQuery request, CancellationToken cancellationToken)
    {
        var list = await _applicationRepository.GetApplicationsByFilters(request.StudentId, request.ApplicationNumberId, request.ServiceId, request.Status);

        if (list != null && list.Count > 0)
        {
            List<ApplicationDto> listDto = _mapper.Map<List<ApplicationDto>>(list);
            string studentId = "";
            ResultT<StudentDto> studentDto = new(new());

            foreach (var item in listDto)
            {
                if (studentId != item.StudentId.ToString())
                {
                    studentDto = await _accountService.GetStudentById(item.StudentId.ToString());
                    studentId = item.StudentId.ToString();
                }

                if (studentDto != null && studentDto.Success)
                {
                    item.StudentName = studentDto.Data!.FirstName + " " + studentDto.Data.LastName;
                    item.StudentIdentification = studentDto.Data!.StudentId;
                }
            }

            return new ResultT<List<ApplicationDto>>(true, "Retrieving applications of the system with the filters specified.", listDto);
        }

        return new ResultT<List<ApplicationDto>>(false, "There are not applications in the system with the filters specified", null!);
    }
}
