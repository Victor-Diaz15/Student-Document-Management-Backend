using Microsoft.AspNetCore.Identity;

namespace StudentDocumentManagement.Infrastructure.Identity.Entities;

public class UserApp : IdentityUser
{
    public string IdentityCard { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Rol { get; set; }
    public string ProfilePicture { get; set; } = string.Empty;
}
