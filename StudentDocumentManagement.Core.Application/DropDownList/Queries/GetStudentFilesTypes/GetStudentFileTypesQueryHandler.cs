using StudentDocumentManagement.Core.Application.Dtos.DropDownLists;
using StudentDocumentManagement.Core.Application.Enums;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Application.Shared.Results;
using StudentDocumentManagement.Core.Domain.Enums;
using System.Text.RegularExpressions;

namespace StudentDocumentManagement.Core.Application.DropDownList.Queries.GetStudentFilesTypes;

internal class GetStudentFileTypesQueryHandler : IQueryHandler<GetStudentFileTypesQuery, List<DropDownListDto>>
{
    public Task<ResultT<List<DropDownListDto>>> Handle(GetStudentFileTypesQuery request, CancellationToken cancellationToken)
    {
        var fileTypes = Enum.GetValues(typeof(StudentFileType))
                       .Cast<StudentFileType>()
                       .Select(r => new DropDownListDto
                       {
                           Id = (int)r,
                           //Este regex le agrega espacio entre mayusculas, toma las palabras asi (RoleEjemplo) y retorna el texto (Role Ejemplo)
                           Name = Regex.Replace(r.ToString(), "(?<!^)([A-Z])", " $1")
                       }).ToList();

        var result = new ResultT<List<DropDownListDto>>(true, "Retrieving Student File Types", fileTypes);

        return Task.FromResult(result);
    }
}
