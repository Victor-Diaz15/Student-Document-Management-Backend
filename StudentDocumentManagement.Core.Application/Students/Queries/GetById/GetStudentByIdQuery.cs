using StudentDocumentManagement.Core.Application.Dtos.Accounts;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;

namespace StudentDocumentManagement.Core.Application.Students.Queries.GetById;

public sealed record GetStudentByIdQuery(string Id) : IQuery<StudentDto>;
