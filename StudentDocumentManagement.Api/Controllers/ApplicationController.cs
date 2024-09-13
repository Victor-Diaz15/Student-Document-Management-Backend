using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentDocumentManagement.Api.Filters;
using StudentDocumentManagement.Core.Application.Applications.Commands.AddApplication;
using StudentDocumentManagement.Core.Application.Applications.Queries.GetAllApplications;
using StudentDocumentManagement.Core.Application.Applications.Queries.GetApplicationByFilters;
using StudentDocumentManagement.Core.Application.Applications.Queries.GetApplicationById;
using StudentDocumentManagement.Core.Application.Applications.Queries.GetApplicationToUpdate;

namespace StudentDocumentManagement.Api.Controllers;

[Route("api/application")]
[Authorize]
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

    [HttpGet("filters")]
    public async Task<IActionResult> GetAllApplicationsByFilters([FromQuery] GetApplicationsByFiltersQuery query)
    {
        var result = await Sender.Send(query);

        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpGet("{applicationId:guid}")]
    public async Task<IActionResult> GetApplicationById(Guid applicationId)
    {
        var query = new GetApplicationByIdQuery(applicationId);

        var result = await Sender.Send(query);

        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpGet("update/{applicationId:guid}")]
    public async Task<IActionResult> GetApplicationToUpdate(Guid applicationId)
    {
        var query = new GetApplicationToUpdateQuery(applicationId);

        var result = await Sender.Send(query);

        return result.Success ? Ok(result) : NotFound(result);
    }

    [ServiceFilter(
        typeof(
        CommandValidatorFilter<AddApplicationCommand,
            ApplicationController,
            AddApplicationCommandValidator>
        ))]
    [HttpPost()]
    public async Task<IActionResult> AddApplication([FromBody] AddApplicationCommand command)
    {
        _ = await Sender.Send(command);
        return NoContent();
    }

}
