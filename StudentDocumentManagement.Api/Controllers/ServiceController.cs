using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentDocumentManagement.Core.Application.Services.Queries.GetAllServices;

namespace StudentDocumentManagement.Api.Controllers;

[Route("api/service")]
public class ServiceController : ApiController
{
    public ServiceController(ISender sender) : base(sender)
    {
    }

    [HttpGet()]
    public async Task<IActionResult> GetAllServices()
    {
        var query = new GetAllServicesQuery();

        var result = await Sender.Send(query);

        return result.Success ? Ok(result) : BadRequest(result);
    }
}
