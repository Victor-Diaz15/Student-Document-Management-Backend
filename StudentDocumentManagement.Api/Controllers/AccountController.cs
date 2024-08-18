using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentDocumentManagement.Core.Application.Accounts.Commands.DeleteAccount;
using StudentDocumentManagement.Core.Application.Accounts.Commands.Login;

namespace StudentDocumentManagement.Api.Controllers;

[Route("api/account")]
public class AccountController : ApiController
{
    public AccountController(ISender sender) : base(sender)
    {
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand loginCommand)
    {
        var result = await Sender.Send(loginCommand);

        return result.Success ? Ok(result) : BadRequest(new { result.Success, result.Message });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAccount(string id)
    {
        var command = new DeleteAccountCommand(id);
        var result = await Sender.Send(command);

        return result.Success ? NoContent() : BadRequest(result);
    }
   
}
