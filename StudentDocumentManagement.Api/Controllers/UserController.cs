using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentDocumentManagement.Api.Filters;
using StudentDocumentManagement.Core.Application.Students.Commands.RegisterStudent;
using StudentDocumentManagement.Core.Application.Users.Commands.RegisterUser;
using StudentDocumentManagement.Core.Application.Users.Queries.GetAll;
using StudentDocumentManagement.Core.Application.Users.Queries.GetById;

namespace StudentDocumentManagement.Api.Controllers;

[Route("api/user")]
public class UserController(ISender sender) : ApiController(sender)
{
    [HttpGet()]
    public async Task<IActionResult> GetAllUsers()
    {
        var query = new GetAllUsersQuery();
        var result = await Sender.Send(query);

        return result.Success ? Ok(result) : NotFound(new { result.Success, result.Message });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var query = new GetUserByIdQuery(id);
        var result = await Sender.Send(query);

        return result.Success ? Ok(result) : NotFound(new { result.Success, result.Message });
    }

    [ServiceFilter(
        typeof(
        CommandValidatorFilter<RegisterUserCommand, 
            UserController, 
            RegisterUserCommandValidator>
        ))]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromForm] RegisterUserCommand command)
    {
        var result = await Sender.Send(command);

        return result.Success ? NoContent() : BadRequest(result);
    }
}
