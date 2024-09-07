using StudentDocumentManagement.Core.Application.Dtos.DropDownLists;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;

namespace StudentDocumentManagement.Core.Application.DropDownList.Queries.GetRoles;

public sealed record GetRolesQuery : IQuery<List<DropDownListDto>>;
