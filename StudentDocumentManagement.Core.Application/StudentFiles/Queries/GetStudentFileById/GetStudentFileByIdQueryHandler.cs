using StudentDocumentManagement.Core.Application.Dtos.StudentFiles;
using StudentDocumentManagement.Core.Application.Helpers;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Application.Shared.Results;
using StudentDocumentManagement.Core.Domain.Abstractions;
using StudentDocumentManagement.Core.Domain.Entities;

namespace StudentDocumentManagement.Core.Application.StudentFiles.Queries.GetStudentFileById;

internal class GetStudentFileByIdQueryHandler : IQueryHandler<GetStudentFileByIdQuery, StudentFileDto>
{
    private readonly IStudentFileRepository _studentFileRepository;

    public GetStudentFileByIdQueryHandler(IStudentFileRepository studentFileRepository)
    {
        _studentFileRepository = studentFileRepository;
    }

    public async Task<ResultT<StudentFileDto>> Handle(GetStudentFileByIdQuery request, CancellationToken cancellationToken)
    {
        var file = await _studentFileRepository.GetByIdAsync(request.Id);
        if (file is not null)
        {
            var fileDto = new StudentFileDto()
            {
                Id = file.Id,
                StudentId = file.StudentId,
                FileUrl = file.Url,
                FileType = StudentFileFuncionts.GetFileType(file.FileType),
                Status = StudentFileFuncionts.GetFileStatus(file.Status)
            };

            return new ResultT<StudentFileDto>(true, "Retrieving Student File", fileDto);
        }

        return new ResultT<StudentFileDto>(false,
            $"No existe archivo de id: { request.Id }",
            null!);
    }
}
