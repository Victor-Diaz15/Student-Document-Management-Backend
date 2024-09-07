using StudentDocumentManagement.Core.Application.Dtos.DropDownLists;
using StudentDocumentManagement.Core.Application.Enums;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Application.Shared.Results;
using System.Text.RegularExpressions;

namespace StudentDocumentManagement.Core.Application.DropDownList.Queries.GetRoles;

internal class GetRolesQueryHandler : IQueryHandler<GetRolesQuery, List<DropDownListDto>>
{
    public Task<ResultT<List<DropDownListDto>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = Enum.GetValues(typeof(Roles))
                       .Cast<Roles>()
                       .Select(r => new DropDownListDto
                       {
                           Id = (int)r,
                           //Este regex le agrega espacio entre mayusculas, toma las palabras asi (RoleEjemplo) y retorna el texto (Role Ejemplo)
                           Name = Regex.Replace(r.ToString(), "(?<!^)([A-Z])", " $1") 
                       }).ToList();

        var result = new ResultT<List<DropDownListDto>>(true, "Retrieving Roles", roles);

        return Task.FromResult(result);
    }
}
