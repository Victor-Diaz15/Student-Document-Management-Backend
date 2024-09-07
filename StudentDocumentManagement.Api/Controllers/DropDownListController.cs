using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentDocumentManagement.Core.Application.DropDownList.Queries.GetRoles;
using StudentDocumentManagement.Core.Application.DropDownList.Queries.GetStudentFilesTypes;

namespace StudentDocumentManagement.Api.Controllers;

[Route("api/dropdownlist")]
public class DropDownListController : ApiController
{
    public DropDownListController(ISender sender) : base(sender)
    {
    }

    [HttpGet("roles")]
    public async Task<IActionResult> GetRoles()
    {
        var query = new GetRolesQuery();
        
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpGet("studentFileTypes")]
    public async Task<IActionResult> GetStudentFiles()
    {
        var query = new GetStudentFileTypesQuery();

        var result = await Sender.Send(query);

        return Ok(result);
    }
}
