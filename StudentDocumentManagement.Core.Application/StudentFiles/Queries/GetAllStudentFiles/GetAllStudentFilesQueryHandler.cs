using StudentDocumentManagement.Core.Application.Dtos.StudentFiles;
using StudentDocumentManagement.Core.Application.Helpers;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Application.Shared.Results;
using StudentDocumentManagement.Core.Domain.Abstractions;
using StudentDocumentManagement.Core.Domain.Enums;

namespace StudentDocumentManagement.Core.Application.StudentFiles.Queries.GetAllStudentFiles;

internal class GetAllStudentFilesQueryHandler : IQueryHandler<GetAllStudentFilesQuery, List<StudentFileDto>>
{
    private readonly IStudentFileRepository _studentFileRepository;

    public GetAllStudentFilesQueryHandler(IStudentFileRepository studentFileRepository)
    {
        _studentFileRepository = studentFileRepository;
    }

    public async Task<ResultT<List<StudentFileDto>>> Handle(GetAllStudentFilesQuery request, CancellationToken cancellationToken)
    {
        List<StudentFileDto> studentFilesDtos = [];

        //Logica
        var studentFiles = await _studentFileRepository.GetAllStudentFilesAsync(request.StudentId);

        if(studentFiles != null)
        {
            foreach(var studentFile in studentFiles)
            {
                studentFilesDtos.Add(new StudentFileDto()
                {
                    Id = studentFile.Id,
                    StudentId = studentFile.StudentId,
                    FileUrl = studentFile.Url,
                    FileType = studentFile.FileType.ToString()!,
                    Status = studentFile.Status.ToString()
                });
            }

            return new ResultT<List<StudentFileDto>>(true, "Retrieving Student Files", studentFilesDtos);

        }

        return new ResultT<List<StudentFileDto>>(false, 
            "No existen archivos en el sistema pertenecientes al usuario proporcionado", 
            studentFilesDtos);
    }
}
