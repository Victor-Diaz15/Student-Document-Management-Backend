using StudentDocumentManagement.Core.Application.Dtos.DropDownLists;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;

namespace StudentDocumentManagement.Core.Application.DropDownList.Queries.GetStudentFilesTypes;

public sealed record GetStudentFileTypesQuery : IQuery<List<DropDownListDto>>;
