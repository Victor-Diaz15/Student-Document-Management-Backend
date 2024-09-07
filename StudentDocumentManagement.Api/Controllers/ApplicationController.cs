using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentDocumentManagement.Api.Filters;
using StudentDocumentManagement.Core.Application.Applications.Commands.AddApplication;
using StudentDocumentManagement.Core.Application.Applications.Queries.GetAllApplications;
using StudentDocumentManagement.Core.Application.Students.Commands.RegisterStudent;

namespace StudentDocumentManagement.Api.Controllers;

[Route("api/application")]
public class ApplicationController : ApiController
{
    public ApplicationController(ISender sender) : base(sender)
    {
    }


    [HttpGet]
    public async Task<IActionResult> GetAllApplications()
    {
        var query = new GetAllApplicationsQuery();

        var result = await Sender.Send(query);

        return result.Success ? Ok(result) : NotFound(result);
    }


    [ServiceFilter(
        typeof(
        CommandValidatorFilter<AddApplicationCommand,
            ApplicationController,
            AddApplicationCommandValidator>
        ))]
    [Authorize]
    [HttpPost()]
    public async Task<IActionResult> AddApplication([FromBody] AddApplicationCommand command)
    {
        _ = await Sender.Send(command);
        return NoContent();
    }

}
