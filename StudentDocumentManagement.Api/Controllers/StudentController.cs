using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentDocumentManagement.Api.Filters;
using StudentDocumentManagement.Core.Application.Accounts.Commands.Login;
using StudentDocumentManagement.Core.Application.Students.Commands.RegisterStudent;
using StudentDocumentManagement.Core.Application.Students.Queries.GetAll;
using StudentDocumentManagement.Core.Application.Students.Queries.GetById;
using StudentDocumentManagement.Core.Application.Students.Queries.GetByStudentId;
using StudentDocumentManagement.Core.Application.Users.Commands.RegisterUser;
using StudentDocumentManagement.Core.Application.Users.Queries.GetById;

namespace StudentDocumentManagement.Api.Controllers;

[Route("api/student")]
public class StudentController : ApiController
{
    public StudentController(ISender sender) : base(sender)
    {
    }

    [HttpGet()]
    public async Task<IActionResult> GetAllStudents()
    {
        var query = new GetAllStudentsQuery();
        var result = await Sender.Send(query);

        return result.Success ? Ok(result) : NotFound(new { result.Success, result.Message });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudentById(string id)
    {
        var query = new GetStudentByIdQuery(id);
        var result = await Sender.Send(query);

        return result.Success ? Ok(result) : NotFound(new { result.Success, result.Message });
    }

    [HttpGet("studentId/{studentId}")]
    public async Task<IActionResult> GetStudentByStudentId(string studentId)
    {
        var query = new GetStudentByStudentIdQuery(studentId);
        var result = await Sender.Send(query);

        return result.Success ? Ok(result) : NotFound(new { result.Success, result.Message });
    }

    [ServiceFilter(
        typeof(
        CommandValidatorFilter<RegisterStudentCommand, 
            StudentController, 
            RegisterStudentCommandValidator>
        ))]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterStudent([FromForm] RegisterStudentCommand command)
    {
        var result = await Sender.Send(command);

        return result.Success ? NoContent() : BadRequest(result);
    }
}
