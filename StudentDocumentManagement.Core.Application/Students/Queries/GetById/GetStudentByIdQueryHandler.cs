using StudentDocumentManagement.Core.Application.Dtos.Accounts;
using StudentDocumentManagement.Core.Application.Interfaces;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Application.Shared.Results;

namespace StudentDocumentManagement.Core.Application.Students.Queries.GetById;

internal class GetStudentByIdQueryHandler : IQueryHandler<GetStudentByIdQuery, StudentDto>
{

    private readonly IAccountService _accountService;

    public GetStudentByIdQueryHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task<ResultT<StudentDto>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _accountService.GetStudentById(request.Id);

        return result;
    }
}
