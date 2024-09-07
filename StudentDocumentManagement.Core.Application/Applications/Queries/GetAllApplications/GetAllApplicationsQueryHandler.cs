using AutoMapper;
using StudentDocumentManagement.Core.Application.Dtos.Accounts;
using StudentDocumentManagement.Core.Application.Dtos.Applications;
using StudentDocumentManagement.Core.Application.Interfaces;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Application.Shared.Results;
using StudentDocumentManagement.Core.Domain.Abstractions;

namespace StudentDocumentManagement.Core.Application.Applications.Queries.GetAllApplications;

internal class GetAllApplicationsQueryHandler : IQueryHandler<GetAllApplicationsQuery, List<ApplicationDto>>
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IMapper _mapper;
    private readonly IAccountService _accountService;
    public GetAllApplicationsQueryHandler(IApplicationRepository applicationRepository, IMapper mapper, IAccountService accountService)
    {
        _applicationRepository = applicationRepository;
        _mapper = mapper;
        _accountService = accountService;
    }

    public async Task<ResultT<List<ApplicationDto>>> Handle(GetAllApplicationsQuery request, CancellationToken cancellationToken)
    {
        var list = await _applicationRepository.GetAllWithIncludeAndThenInclude();

        if(list != null &&  list.Count > 0)
        {
            List<ApplicationDto> listDto = _mapper.Map<List<ApplicationDto>>(list);
            string studentId = "";
            ResultT<StudentDto> studentDto = new(new());

            foreach(var item in listDto)
            {
                if(studentId != item.StudentId.ToString())
                {
                    studentDto = await _accountService.GetStudentById(item.StudentId.ToString());
                    studentId = item.StudentId.ToString();
                }

                if(studentDto != null && studentDto.Success)
                {
                    item.StudentName = studentDto.Data!.FirstName + " " + studentDto.Data.LastName;
                    item.StudentIdentification = studentDto.Data!.StudentId;
                }
            }

            return new ResultT<List<ApplicationDto>>(true, "Retrieving applications of the system.", listDto);
        }

        return new ResultT<List<ApplicationDto>>(false, "There are not applications in the system.", null!);
    }
}
