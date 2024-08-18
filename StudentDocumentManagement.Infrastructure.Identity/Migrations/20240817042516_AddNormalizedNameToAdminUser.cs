using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentDocumentManagement.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddNormalizedNameToAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3327aeb1-f58b-4f70-a141-f33ebc124629",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8205c88f-c12f-4cee-ab66-2a55e5e3daa2", "ADMIN", "AQAAAAIAAYagAAAAEBFJEb57vUg7kackzmfZp+v1JiOmVkhzL9tB5MUG+7J40a2UwihhyWPoEDmidA7bXQ==", "6deba2fe-cd2f-4213-b62e-e9bd19fbd848" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3327aeb1-f58b-4f70-a141-f33ebc124629",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5d48caef-786b-4713-81a1-239e2df90379", null, "AQAAAAIAAYagAAAAEEozkoWCCeIalBxiNaprD0kBrZipqK4UVa2/UlQz4yMMJnlYNxZyye/UCGX1rmMhIQ==", "d2fc4376-bf33-453c-b57f-5f6e32a5741d" });
        }
    }
}
