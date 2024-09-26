using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentDocumentManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatecolumnApplicationNumberId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ApplicationNumberId",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ApplicationNumberId",
                table: "Applications",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
