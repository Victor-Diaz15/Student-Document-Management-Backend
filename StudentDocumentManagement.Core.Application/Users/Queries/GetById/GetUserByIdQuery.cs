using StudentDocumentManagement.Core.Application.Dtos.Accounts;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;

namespace StudentDocumentManagement.Core.Application.Users.Queries.GetById;

public sealed record GetUserByIdQuery(string UserId) : IQuery<UserDto>;
