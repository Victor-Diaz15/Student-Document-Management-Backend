using StudentDocumentManagement.Core.Application.Dtos.StudentFiles;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;

namespace StudentDocumentManagement.Core.Application.StudentFiles.Queries.GetStudentFileById;

public sealed record GetStudentFileByIdQuery(Guid Id) : IQuery<StudentFileDto>;
