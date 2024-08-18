namespace StudentDocumentManagement.Core.Application.Dtos.Accounts;

public class UserDto
{
    public string Id { get; set; } = string.Empty;
    public string IdentityCard { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string ProfilePicture { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Rol { get; set; } = string.Empty;
}
