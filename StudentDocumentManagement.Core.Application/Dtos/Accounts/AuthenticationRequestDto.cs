namespace StudentDocumentManagement.Core.Application.Dtos.Accounts;

public class AuthenticationRequestDto
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
