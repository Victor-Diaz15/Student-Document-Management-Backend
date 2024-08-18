using StudentDocumentManagement.Core.Application.Dtos.Accounts;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;

namespace StudentDocumentManagement.Core.Application.Users.Queries.GetAll;

public sealed record GetAllUsersQuery : IQuery<List<UserDto>>
{
}
