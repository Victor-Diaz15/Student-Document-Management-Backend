using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentDocumentManagement.Api.Filters;
using StudentDocumentManagement.Core.Application.StudentFiles.Commands.AddStudentFile;
using StudentDocumentManagement.Core.Application.StudentFiles.Commands.ChangeStatus;
using StudentDocumentManagement.Core.Application.StudentFiles.Commands.UpdateStudentFile;
using StudentDocumentManagement.Core.Application.StudentFiles.Queries.GetAllStudentFiles;
using StudentDocumentManagement.Core.Application.StudentFiles.Queries.GetStudentFileById;

namespace StudentDocumentManagement.Api.Controllers;

[Route("api/studentFile")]
public class StudentFileController : ApiController
{
    public StudentFileController(ISender sender) : base(sender){}

    [Authorize]
    [HttpGet("list/{studentId:guid}")]
    public async Task<IActionResult> GetAllStudentFiles(Guid studentId)
    {
        var query = new GetAllStudentFilesQuery(studentId);
        var result = await Sender.Send(query);

        return result.Success ? Ok(result) : NotFound(result);
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetStudentFileById(Guid id)
    {
        var query = new GetStudentFileByIdQuery(id);
        var result = await Sender.Send(query);

        return result.Success ? Ok(result) : NotFound(result);
    }

    [Authorize]
    [ServiceFilter(
        typeof(
        CommandValidatorFilter<AddStudentFileCommand,
            StudentFileController,
            AddStudentFileCommandValidator>
        ))]
    [HttpPost("add-file")]
    public async Task<IActionResult> AddStudentFile([FromForm] AddStudentFileCommand command)
    {
        var result = await Sender.Send(command);

        return result.Success ? Created("", result) : BadRequest(result);
    }

    [Authorize]
    [ServiceFilter(
        typeof(
        CommandValidatorFilter<UpdateStudentFileCommand,
            StudentFileController,
            UpdateStudentFileCommandValidator>
        ))]
    [HttpPut("update-file/{fileId:guid}")]
    public async Task<IActionResult> UpdateStudentFile(Guid fileId, [FromForm] UpdateStudentFileCommand command)
    {
        command.FileId = fileId;
        var result = await Sender.Send(command);

        return result.Success ? NoContent() : NotFound(result);
    }

    [Authorize]
    [ServiceFilter(
        typeof(
        CommandValidatorFilter<ChangeStatusStudentFileCommand,
            StudentFileController,
            ChangeStatusStudentFileCommandValidator>
        ))]
    [HttpPatch("change-status/{fileId:guid}")]
    public async Task<IActionResult> ChangeStatusStudentFile(Guid fileId, [FromBody] ChangeStatusStudentFileCommand command)
    {
        command.FileId = fileId;
        var result = await Sender.Send(command);

        return result.Success ? NoContent() : NotFound(result);
    }
}
