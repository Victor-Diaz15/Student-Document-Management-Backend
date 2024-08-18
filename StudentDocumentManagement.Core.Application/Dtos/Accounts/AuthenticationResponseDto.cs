namespace StudentDocumentManagement.Core.Application.Dtos.Accounts;

public class AuthenticationResponseDto
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string JWTToken { get; set; } = string.Empty;
}
