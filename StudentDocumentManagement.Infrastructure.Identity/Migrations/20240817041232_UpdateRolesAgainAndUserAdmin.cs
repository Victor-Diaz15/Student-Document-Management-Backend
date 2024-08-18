using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentDocumentManagement.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRolesAgainAndUserAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "04941844-208e-4bb9-9a07-0b427cbbeb23", null, "Reception", "RECEPTION" },
                    { "6097134b-23d7-4388-adae-3555823fba22", null, "DepartmentalManager", "DEPARTMENTALMANAGER" },
                    { "bd4d6f9e-4cf8-4445-8d8e-96d02ae316db", null, "Student", "STUDENT" }
                });

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3327aeb1-f58b-4f70-a141-f33ebc124629",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "5d48caef-786b-4713-81a1-239e2df90379", "AQAAAAIAAYagAAAAEEozkoWCCeIalBxiNaprD0kBrZipqK4UVa2/UlQz4yMMJnlYNxZyye/UCGX1rmMhIQ==", "d2fc4376-bf33-453c-b57f-5f6e32a5741d", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "04941844-208e-4bb9-9a07-0b427cbbeb23");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6097134b-23d7-4388-adae-3555823fba22");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd4d6f9e-4cf8-4445-8d8e-96d02ae316db");

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7d2a8aad-a111-4eef-8475-69c541fb7c03", null, "DepartmentalManager", "DEPARTMENTALMANAGER" },
                    { "820a79b5-cd85-45b8-8e53-4af4e7ba5de5", null, "Reception", "RECEPTION" },
                    { "8b8f289e-144f-47b5-ae83-6f2ad9fec75f", null, "Student", "STUDENT" }
                });

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3327aeb1-f58b-4f70-a141-f33ebc124629",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "62e6a447-bd25-4c62-9536-8128775d258d", "AQAAAAIAAYagAAAAEObmQYAfs/ZOkMQaUkcUur+neT+TE2d0VkijY62R0G2CmgGhlyGtQ+gs3AvzanPN0A==", "edce6239-5ec4-4ecd-bff5-2881b8f491e0", "Admin" });
        }
    }
}
