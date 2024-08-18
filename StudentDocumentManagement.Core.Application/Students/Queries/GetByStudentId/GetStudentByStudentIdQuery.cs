using StudentDocumentManagement.Core.Application.Dtos.Accounts;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;

namespace StudentDocumentManagement.Core.Application.Students.Queries.GetByStudentId;

public sealed record GetStudentByStudentIdQuery(string StudentId) : IQuery<StudentDto>;
