using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentDocumentManagement.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRolesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ad29226-0d83-45a0-8ceb-7d6f1bab5c35");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7b159d3f-dc98-43a3-9026-2caa1a48d6d0");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c78ebff-eff6-4ca0-b4d7-c56c6ebc5add");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "26bb0fef-8cc4-4435-8610-b4dd754b96d2", "9c9a7cba-5dfa-4f52-a5cd-60dd184c6580" });

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "26bb0fef-8cc4-4435-8610-b4dd754b96d2");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9c9a7cba-5dfa-4f52-a5cd-60dd184c6580");

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "47d0e6c3-ac34-4ce9-908b-0a5ec3c10bd5", null, "Admin", "ADMIN" },
                    { "7d2a8aad-a111-4eef-8475-69c541fb7c03", null, "DepartmentalManager", "DEPARTMENTALMANAGER" },
                    { "820a79b5-cd85-45b8-8e53-4af4e7ba5de5", null, "Reception", "RECEPTION" },
                    { "8b8f289e-144f-47b5-ae83-6f2ad9fec75f", null, "Student", "STUDENT" }
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IdentityCard", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "Rol", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "3327aeb1-f58b-4f70-a141-f33ebc124629", 0, "62e6a447-bd25-4c62-9536-8128775d258d", "DefaultAdminUser@gmail.com", true, "Admin", "40219908787", "User", false, null, null, null, "AQAAAAIAAYagAAAAEObmQYAfs/ZOkMQaUkcUur+neT+TE2d0VkijY62R0G2CmgGhlyGtQ+gs3AvzanPN0A==", "8093456754", true, "", 0, "edce6239-5ec4-4ecd-bff5-2881b8f491e0", false, "Admin", "UserApp" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "47d0e6c3-ac34-4ce9-908b-0a5ec3c10bd5", "3327aeb1-f58b-4f70-a141-f33ebc124629" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7d2a8aad-a111-4eef-8475-69c541fb7c03");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "820a79b5-cd85-45b8-8e53-4af4e7ba5de5");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b8f289e-144f-47b5-ae83-6f2ad9fec75f");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "47d0e6c3-ac34-4ce9-908b-0a5ec3c10bd5", "3327aeb1-f58b-4f70-a141-f33ebc124629" });

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "47d0e6c3-ac34-4ce9-908b-0a5ec3c10bd5");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3327aeb1-f58b-4f70-a141-f33ebc124629");

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "26bb0fef-8cc4-4435-8610-b4dd754b96d2", null, "Admin", "Admin" },
                    { "4ad29226-0d83-45a0-8ceb-7d6f1bab5c35", null, "Reception", null },
                    { "7b159d3f-dc98-43a3-9026-2caa1a48d6d0", null, "Departmental_Manager", null },
                    { "9c78ebff-eff6-4ca0-b4d7-c56c6ebc5add", null, "Student", null }
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IdentityCard", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "Rol", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "9c9a7cba-5dfa-4f52-a5cd-60dd184c6580", 0, "3598d8b5-611d-40d1-9385-d9ebbc769859", "DefaultAdminUser@gmail.com", true, "Admin", "40219908787", "User", false, null, null, null, "AQAAAAIAAYagAAAAEK8s2ESBx9jvVfmb0IrzTId4JlUDZjDWtOhrXmRkgysvf3/0sclM6bMUpbaCg+uH0g==", null, true, "", 0, "be6d9a30-4873-4cd2-afba-24ad07ca5747", false, "Admin", "UserApp" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "26bb0fef-8cc4-4435-8610-b4dd754b96d2", "9c9a7cba-5dfa-4f52-a5cd-60dd184c6580" });
        }
    }
}
