namespace StudentDocumentManagement.Core.Application.Dtos.Accounts;

public class ResetUserPasswordRequestDto
{
    public string Email { get; set; } = string.Empty;   
    public string Token { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
