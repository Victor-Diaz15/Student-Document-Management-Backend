namespace StudentDocumentManagement.Core.Application.Dtos.Accounts;

public class UserUpdateRequestDto
{

    public string IdentityCard { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public int Rol { get; set; }
    public string ProfilePicture { get; set; } = string.Empty;
}
