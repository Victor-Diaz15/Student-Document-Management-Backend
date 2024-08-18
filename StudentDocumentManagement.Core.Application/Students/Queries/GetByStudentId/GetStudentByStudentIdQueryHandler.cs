using StudentDocumentManagement.Core.Application.Dtos.Accounts;
using StudentDocumentManagement.Core.Application.Interfaces;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Application.Shared.Results;

namespace StudentDocumentManagement.Core.Application.Students.Queries.GetByStudentId;

public class GetStudentByStudentIdQueryHandler : IQueryHandler<GetStudentByStudentIdQuery, StudentDto>
{
    private readonly IAccountService _accountService;

    public GetStudentByStudentIdQueryHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task<ResultT<StudentDto>> Handle(GetStudentByStudentIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _accountService.GetStudentByStudendId(request.StudentId);

        return result;
    }
}
