using StudentDocumentManagement.Core.Application.Dtos.Accounts;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;

namespace StudentDocumentManagement.Core.Application.Students.Queries.GetAll;

public sealed record GetAllStudentsQuery() : IQuery<List<StudentDto>>;
