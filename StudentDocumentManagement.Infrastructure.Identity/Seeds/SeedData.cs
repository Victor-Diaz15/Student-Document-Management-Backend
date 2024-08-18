using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentDocumentManagement.Core.Application.Enums;
using StudentDocumentManagement.Infrastructure.Identity.Entities;
using System.Data;
using System.Reflection.Emit;

namespace StudentDocumentManagement.Infrastructure.Identity.Seeds;

public static class SeedData
{
    private static readonly string userAdminId = "3327aeb1-f58b-4f70-a141-f33ebc124629";
    private static readonly string rolAdminId = "47d0e6c3-ac34-4ce9-908b-0a5ec3c10bd5";

    private static IdentityRole GenerateRol(string rol, string id)
    {

        return new IdentityRole()
        {
            Id = id,
            Name = rol,
            NormalizedName = rol.ToUpper()
        };
    }

    public static void SeedRoles(ModelBuilder mb)
    {
        const string rolStudentId = "bd4d6f9e-4cf8-4445-8d8e-96d02ae316db";
        const string rolReceptionId = "04941844-208e-4bb9-9a07-0b427cbbeb23";
        const string rolDepartamentalManagerId = "6097134b-23d7-4388-adae-3555823fba22";

        mb.Entity<IdentityRole>()
            .HasData(
                GenerateRol(rol: Roles.Admin.ToString(), rolAdminId),
                GenerateRol(Roles.Student.ToString(), rolStudentId),
                GenerateRol(Roles.Reception.ToString(), rolReceptionId),
                GenerateRol(Roles.DepartmentalManager.ToString(), rolDepartamentalManagerId)
            );
    }

    public static void SeedAdmin(ModelBuilder mb)
    {
        var passwordHasher = new PasswordHasher<UserApp>();

        UserApp defaultUser = new();
        defaultUser.Id = userAdminId;
        defaultUser.IdentityCard = "40219908787";
        defaultUser.UserName = "admin";
        defaultUser.NormalizedUserName = "ADMIN";
        defaultUser.Email = "DefaultAdminUser@gmail.com";
        defaultUser.FirstName = "Admin";
        defaultUser.LastName = "User";
        defaultUser.EmailConfirmed = true;
        defaultUser.PhoneNumber = "8093456754";
        defaultUser.PhoneNumberConfirmed = true;
        defaultUser.Rol = (int)Roles.Admin;
        defaultUser.PasswordHash = passwordHasher.HashPassword(defaultUser, "123Pa$$word");

        mb.Entity<UserApp>()
                .HasData(defaultUser);

        mb.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { UserId = userAdminId, RoleId = rolAdminId }
        );
    }
}
