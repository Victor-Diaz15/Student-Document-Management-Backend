using StudentDocumentManagement.Core.Application.Dtos.Accounts;
using StudentDocumentManagement.Core.Application.Shared.Results;

namespace StudentDocumentManagement.Core.Application.Interfaces;

public interface IAccountService
{
    Task<ResultT<AuthenticationResponseDto>> AuthenticationAsync(AuthenticationRequestDto req);
    Task<Result> RegisterUserAsync(RegisterUserRequestDto req);
    Task<ResultT<RegisterStudentResponseDto>> RegisterStudentAsync(RegisterStudentRequestDto req);
    Task<ResultT<List<UserDto>>> GetAllUsers();
    Task<ResultT<List<StudentDto>>> GetAllStudents();
    Task<ResultT<UserDto>> GetUserById(string id);
    Task<ResultT<StudentDto>> GetStudentById(string id);
    Task<ResultT<StudentDto>> GetStudentByStudendId(string studentId);
    Task<Result> UpdateUserAsync(UserUpdateRequestDto req, string id);
    Task<Result> ResetPasswordAsync(ResetUserPasswordRequestDto req);
    Task SignOutAsync();
    Task<Result> DeleteUserAsync(string id);
    Task<ResultT<string>> ConfirmAccountAsync(string userId, string token);
    Task<bool> IsEmailUniqueAsync(string email);
    Task<bool> IsUserNameUniqueAsync(string userName);
    Task<bool> IsStudentIdUniqueAsync(string studentId);
    Task<bool> IsIdentityCardUniqueAsync(string identityCard);
}
