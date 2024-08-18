using StudentDocumentManagement.Core.Application.Dtos.Accounts;
using StudentDocumentManagement.Core.Application.Interfaces;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Application.Shared.Results;

namespace StudentDocumentManagement.Core.Application.Students.Queries.GetAll;

internal class GetAllStudentsQueryHandler : IQueryHandler<GetAllStudentsQuery, List<StudentDto>>
{
    private readonly IAccountService _accountService;

    public GetAllStudentsQueryHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task<ResultT<List<StudentDto>>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    { 
        var result = await _accountService.GetAllStudents();

        return result;
    }
}
