using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentDocumentManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSystemEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    ServiceType = table.Column<int>(type: "int", nullable: false),
                    Borrado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceId);
                });

            migrationBuilder.CreateTable(
                name: "StudentFiles",
                columns: table => new
                {
                    StudentFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileType = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Borrado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentFiles", x => x.StudentFileId);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Borrado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.ApplicationId);
                    table.ForeignKey(
                        name: "FK_Applications_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId");
                });

            migrationBuilder.CreateTable(
                name: "ApplicationsStudentFiles",
                columns: table => new
                {
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationsStudentFiles", x => new { x.StudentFileId, x.ApplicationId });
                    table.ForeignKey(
                        name: "FK_ApplicationsStudentFiles_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "ApplicationId");
                    table.ForeignKey(
                        name: "FK_ApplicationsStudentFiles_StudentFiles_StudentFileId",
                        column: x => x.StudentFileId,
                        principalTable: "StudentFiles",
                        principalColumn: "StudentFileId");
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceId", "Borrado", "Name", "Price", "ServiceType" },
                values: new object[,]
                {
                    { new Guid("197e5630-d2d0-4699-845a-11a78d25a569"), false, "Copia Título Legalizada", 150, 0 },
                    { new Guid("1b06e340-5eee-427d-b0a7-063e17464dc1"), false, "Copia Título Legalizada", 150, 1 },
                    { new Guid("21f6fed9-9fa6-4476-9f22-7d630fb3cec7"), false, "Legalización Completa", 650, 0 },
                    { new Guid("25a9a1fe-b559-4168-b6bc-97f677721397"), false, "Certificación Título Legalizada", 150, 0 },
                    { new Guid("26214a95-18c5-4f18-b936-bbe7cfe72d46"), false, "Legalización Completa", 650, 1 },
                    { new Guid("4999cfc4-661b-48ed-bc60-41f07a855a36"), false, "Récord de Nota Legalizada", 200, 1 },
                    { new Guid("53b6a489-b2e4-4ec4-a248-19e7f3999a29"), false, "Récord de Nota Legalizada", 200, 0 },
                    { new Guid("8d11c889-679f-4d38-854e-9755df726632"), false, "Certificación Título Legalizada", 150, 1 },
                    { new Guid("bad159b3-1582-45a3-a4b9-1bbe736770dd"), false, "Carta de Doctorado Especial Legalizada", 250, 0 },
                    { new Guid("d41173e3-cf37-48dd-898a-9bb6bf65a54a"), false, "Carta de Doctorado Especial Legalizada", 250, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ServiceId",
                table: "Applications",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationsStudentFiles_ApplicationId",
                table: "ApplicationsStudentFiles",
                column: "ApplicationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationsStudentFiles");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "StudentFiles");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
