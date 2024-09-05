using StudentDocumentManagement.Core.Application.Dtos.StudentFiles;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;

namespace StudentDocumentManagement.Core.Application.StudentFiles.Queries.GetAllStudentFiles;

public sealed record GetAllStudentFilesQuery(Guid StudentId) : IQuery<List<StudentFileDto>>;
