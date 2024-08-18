namespace StudentDocumentManagement.Core.Application.Dtos.Accounts;

public class RegisterUserRequestDto : RegisterStudentRequestDto
{
    public string UserName { get; set; } = string.Empty;
}


