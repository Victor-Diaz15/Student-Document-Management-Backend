using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StudentDocumentManagement.Core.Application.Dtos.Accounts;
using StudentDocumentManagement.Core.Application.Enums;
using StudentDocumentManagement.Core.Application.Interfaces;
using StudentDocumentManagement.Core.Application.Shared.Results;
using StudentDocumentManagement.Core.Domain.Settings;
using StudentDocumentManagement.Infrastructure.Identity.Context;
using StudentDocumentManagement.Infrastructure.Identity.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentDocumentManagement.Infrastructure.Identity.Services;

public class AccountService : IAccountService
{
    private readonly SignInManager<UserApp> _signInManager;
    private readonly UserManager<UserApp> _userManager;
    private readonly JWTSettings _jwtSettings;
    private readonly IdentityContext _identityDbContext;


    public AccountService(SignInManager<UserApp> signInManager, UserManager<UserApp> userManager,
        IOptions<JWTSettings> jwtSettings, IdentityContext identityDbContext)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
        _identityDbContext = identityDbContext;
    }

    public async Task<ResultT<AuthenticationResponseDto>> AuthenticationAsync(AuthenticationRequestDto req)
    {
        ResultT<AuthenticationResponseDto> res = new(new());

        var user = await _userManager.FindByNameAsync(req.UserName);
        if (user is null)
        {
            res.Success = false;
            res.Message = $"No Accounts registered with username {req.UserName}.";
            return res;
        }

        var result = await _signInManager.PasswordSignInAsync(user!.UserName!, req.Password, false, lockoutOnFailure: false);
        if (!result.Succeeded)
        {
            res.Success = false;
            res.Message = $"Invalid Credentials for {req.UserName}.";
            return res;
        }
        
        JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);

        res.Message = "Login successfull";
        res.Data!.Id = user.Id;
        res.Data.Email = user.Email!;
        res.Data.JWTToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return res;
    }

    public async Task<Result> RegisterUserAsync(RegisterUserRequestDto req)
    {
        Result res = new();

        var user = new UserApp
        {
            IdentityCard = req.IdentityCard,
            FirstName = req.FirstName,
            LastName = req.LastName,
            UserName = req.UserName,
            Email = req.Email,
            PhoneNumber = req.PhoneNumber,
            Rol = req.Rol,
            ProfilePicture = req.ProfilePicture
        };

        var result = await _userManager.CreateAsync(user, req.Password);

        if (!result.Succeeded)
        {
            res.Success = false;
            res.Message = result.Errors.FirstOrDefault()!.Description ?? "An error occurred when trying to register the user.";
            return res;
        }

        switch (user.Rol)
        {
            case (int)Roles.Reception:
                await _userManager.AddToRoleAsync(user, Roles.Reception.ToString().ToUpper());
                break;

            case (int)Roles.DepartmentalManager:
                await _userManager.AddToRoleAsync(user, Roles.DepartmentalManager.ToString().ToUpper());
                break;

            default:
                await _userManager.AddToRoleAsync(user, Roles.Admin.ToString().ToUpper());
                break;
        }

        res.Success = true;
        res.Message = "User registered.";

        return res;
    }

    public async Task<ResultT<RegisterStudentResponseDto>> RegisterStudentAsync(RegisterStudentRequestDto req)
    {
        ResultT<RegisterStudentResponseDto> res = new(new());

        string studenId = GenerateStudentId();

        var user = new Student
        {
            IdentityCard = req.IdentityCard,
            StudentId = studenId,
            FirstName = req.FirstName,
            LastName = req.LastName,
            UserName = studenId,
            Email = req.Email,
            PhoneNumber = req.PhoneNumber,
            Rol = (int)Roles.Student,
            ProfilePicture = req.ProfilePicture
        };

        var result = await _userManager.CreateAsync(user, req.Password);

        if (!result.Succeeded)
        {
            res.Success = false;
            res.Message = result.Errors.FirstOrDefault()!.Description ?? "An error occurred when trying to register the user.";
            return res;
        }

        await _userManager.AddToRoleAsync(user, Roles.Student.ToString().ToUpper());

        res.Success = true;
        res.Message = "Student registered.";
        res.Data!.Id = user.Id;
        res.Data.Username = user.UserName;
        return res;
    }

    public async Task<Result> ResetPasswordAsync(ResetUserPasswordRequestDto req)
    {
        Result res = new();

        var user = await _userManager.FindByEmailAsync(req.Email);
        if (user == null)
        {
            res.Success = false;
            res.Message = $"No user registered with {req.Email}.";
            return res;
        }

        req.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(req.Token));
        var result = await _userManager.ResetPasswordAsync(user, req.Token, req.Password);
        if (!result.Succeeded)
        {
            res.Success = false;
            res.Message = result.Errors.FirstOrDefault()!.Description ?? "An error occurred when trying to reset the user password.";
            return res;
        }

        res.Success = true;
        return res;
    }

    public async Task<ResultT<List<UserDto>>> GetAllUsers()
    {
        ResultT<List<UserDto>> res = new(null!);

        var users = await _userManager.Users.ToListAsync();

        var userDtos = new List<UserDto>();

        foreach (var user in users)
        {
            var userDto = new UserDto()
            {
                Id = user.Id,
                IdentityCard = user.IdentityCard,
                UserName = user.UserName!,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber!,
                Rol = await GetUserRole(user),
                ProfilePicture = user.ProfilePicture,
            };

            userDtos.Add(userDto);
        }

        if (users is null)
        {
            res.Success = false;
            res.Message = "There are not users in the system.";
            return res;
        }

        res.Success = true;
        res.Message = "Retrieving the users of the system.";
        res.Data = userDtos;
        return res;
    }

    public async Task<ResultT<UserDto>> GetUserById(string id)
    {
        ResultT<UserDto> res = new(new());

        UserApp? user = await _userManager.FindByIdAsync(id);

        if (user != null)
        {
            res.Data!.Id = user.Id;
            res.Data.IdentityCard = user.IdentityCard;
            res.Data.Email = user.Email!;
            res.Data.FirstName = user.FirstName;
            res.Data.LastName = user.LastName;
            res.Data.UserName = user.UserName!;
            res.Data.PhoneNumber = user.PhoneNumber!;
            res.Data.Rol = await GetUserRole(user);
            res.Data.ProfilePicture = user.ProfilePicture;

            res.Success = true;
            res.Message = $"Retrieving the user with id: {id}";
            return res;
        }

        res.Success = false;
        res.Message = $"Not user exists with this id: {id}";
        return res;
    }

    public async Task<ResultT<List<StudentDto>>> GetAllStudents()
    {
        ResultT<List<StudentDto>> res = new(null!);

        var students = await _identityDbContext.Students
            .Where(s => s.Rol == (int)Roles.Student).ToListAsync();

        var studentDtos = new List<StudentDto>();

        foreach (var user in students)
        {
            var userDto = new StudentDto()
            {
                Id = user.Id,
                IdentityCard = user.IdentityCard,
                StudentId = user.StudentId,
                UserName = user.UserName!,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber!,
                Rol = await GetUserRole(user),
                ProfilePicture = user.ProfilePicture,
            };

            studentDtos.Add(userDto);
        }

        if (students is null)
        {
            res.Success = false;
            res.Message = "There are not students in the system.";
            return res;
        }

        res.Success = true;
        res.Message = "Retrieving the students of the system.";
        res.Data = studentDtos;
        return res;
    }

    public async Task<ResultT<StudentDto>> GetStudentById(string id)
    {
        ResultT<StudentDto> res = new(new());

        Student? student = await _identityDbContext.Students.FindAsync(id);

        if (student != null)
        {
            res.Data!.Id = student.Id;
            res.Data.IdentityCard = student.IdentityCard;
            res.Data.StudentId = student.StudentId;
            res.Data.Email = student.Email!;
            res.Data.FirstName = student.FirstName;
            res.Data.LastName = student.LastName;
            res.Data.UserName = student.UserName!;
            res.Data.PhoneNumber = student.PhoneNumber!;
            res.Data.Rol = await GetUserRole(student);
            res.Data.ProfilePicture = student.ProfilePicture;

            res.Success = true;
            res.Message = $"Retrieving the student with id: {id}";
            return res;
        }

        res.Success = false;
        res.Message = $"Not student exists with this id: {id}";
        return res;
    }

    public async Task<ResultT<StudentDto>> GetStudentByStudendId(string studentId)
    {
        ResultT<StudentDto> res = new(new());

        Student? student = await _identityDbContext.Students
            .Where(x => x.StudentId == studentId)
            .FirstOrDefaultAsync();

        if (student != null)
        {
            res.Data!.Id = student.Id;
            res.Data.IdentityCard = student.IdentityCard;
            res.Data.StudentId = student.StudentId;
            res.Data.Email = student.Email!;
            res.Data.FirstName = student.FirstName;
            res.Data.LastName = student.LastName;
            res.Data.UserName = student.UserName!;
            res.Data.PhoneNumber = student.PhoneNumber!;
            res.Data.Rol = await GetUserRole(student);
            res.Data.ProfilePicture = student.ProfilePicture;

            res.Success = true;
            res.Message = $"Retrieving the student with student id: {studentId}";
            return res;
        }

        res.Success = false;
        res.Message = $"Not student exists with this student id: {studentId}";
        return res;
    }

    public async Task<Result> UpdateUserAsync(UserUpdateRequestDto req, string id)
    {
        Result res = new();

        UserApp? user = await _userManager.FindByIdAsync(id);
        if (user != null)
        {
            user.IdentityCard = req.IdentityCard;
            user.FirstName = req.FirstName;
            user.LastName = req.LastName;
            user.UserName = req.UserName;
            user.Email = req.Email;
            user.PhoneNumber = req.PhoneNumber;
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, req.Password);
            user.ProfilePicture = req.ProfilePicture;

            var userUpdated = await _userManager.UpdateAsync(user);
            if (!userUpdated.Succeeded)
            {
                res.Success = false;
                res.Message = userUpdated.Errors.FirstOrDefault()!.Description ?? "An error occurred when trying to update the user."; ;
                return res;

            }

            res.Success = true;
            res.Message = "User updated";
            return res;
        }
        else
        {
            res.Success = false;
            res.Message = $"No accounts exists with this id: {id}";
            return res;
        }
    }

    public async Task<ResultT<string>> ConfirmAccountAsync(string userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return new ResultT<string>("No Accounts registered with this user");
        }

        token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded)
        {
            return new ResultT<string>($"An error occurred while confirming {user.Email}.");
        }

        return new ResultT<string>($"Account confirmed for {user.Email}. You can now use the app");
    }

    public async Task<Result> DeleteUserAsync(string id)
    {
        UserApp? user = await _userManager.FindByIdAsync(id);
        var result = await _userManager.DeleteAsync(user!);

        if (!result.Succeeded)
        {
            return new Result(false, "The account could not be deleted.");
        }

        return new Result(true, "Account deleted.");
    }

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<bool> IsEmailUniqueAsync(string email)
    {
        return await _identityDbContext.Users.AnyAsync(u => u.NormalizedEmail == email.ToUpper());
    }

    public async Task<bool> IsUserNameUniqueAsync(string userName)
    {
        return await _identityDbContext.Users.AnyAsync(x => x.NormalizedUserName == userName.ToUpper());
    }

    public async Task<bool> IsIdentityCardUniqueAsync(string identityCard)
    {
        return await _identityDbContext.Users.AnyAsync(x => x.IdentityCard == identityCard);
    }

    #region private methods

    private async Task<string> GetUserRole(UserApp user)
    {
        var roles = await _userManager.GetRolesAsync(user);

        return roles.FirstOrDefault()!;
    }
    private async Task<JwtSecurityToken> GenerateJWToken(UserApp user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = new List<Claim>();

        foreach (var role in roles)
        {
            roleClaims.Add(new Claim("roles", role));
        }

        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Name, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim("uid",user.Id)
        }
        .Union(userClaims)
        .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);

        return jwtSecurityToken;
    }

    private string GenerateStudentId()
    {
        // Obtén el último número de matrícula generado
        var lastStudentId = _identityDbContext.Students
            .OrderByDescending(s => s.StudentId)
            .Select(s => s.StudentId)
            .FirstOrDefault();

        // Si no hay números anteriores, empieza desde STU-0001
        if (string.IsNullOrEmpty(lastStudentId))
        {
            return "STU-0001";
        }

        // Extrae la parte numérica del último número de matrícula
        var lastNumber = int.Parse(lastStudentId.Substring(4));
        var newNumber = lastNumber + 1;

        // Genera el nuevo número de matrícula
        return $"STU-{newNumber:D4}";
    }

    #endregion
}
