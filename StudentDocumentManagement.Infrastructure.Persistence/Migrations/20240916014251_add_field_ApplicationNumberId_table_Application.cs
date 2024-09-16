using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentDocumentManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class add_field_ApplicationNumberId_table_Application : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationNumberId",
                table: "Applications",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationNumberId",
                table: "Applications");
        }
    }
}
