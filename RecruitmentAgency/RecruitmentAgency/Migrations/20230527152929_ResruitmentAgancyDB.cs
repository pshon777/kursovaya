using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecruitmentAgency.Migrations
{
    /// <inheritdoc />
    public partial class ResruitmentAgancyDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applicants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Surname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vacancy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacancy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VacancyApplication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    VacancyId = table.Column<int>(type: "int", nullable: false),
                    ApplicantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacancyApplication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VacancyApplication_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VacancyApplication_Vacancy_VacancyId",
                        column: x => x.VacancyId,
                        principalTable: "Vacancy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Applicants",
                columns: new[] { "Id", "Email", "Name", "Patronymic", "Surname", "Telephone" },
                values: new object[,]
                {
                    { 90, "Ivanov@mail.ru", "Иван", "Иванович", "Иванов", "7874574" },
                    { 91, "Vas@mail.ru", "Дмитрий", "Иванович", "Васичкин", "4596236" }
                });

            migrationBuilder.InsertData(
                table: "Vacancy",
                columns: new[] { "Id", "Description", "Name", "Salary", "Specialization" },
                values: new object[,]
                {
                    { 90, "Частичная занятость, полный день. Возможна подработка: сменами по 4 - 6 часов, по выходным или по вечерам", "Курьер", 800m, "опыт работы - не требйется" },
                    { 91, "полная занятость, сменный график", "Администратор фитнес-клуба", 1400m, "требуемый опыт работы: 1–3 года" }
                });

            migrationBuilder.InsertData(
                table: "VacancyApplication",
                columns: new[] { "Id", "ApplicantId", "VacancyId" },
                values: new object[,]
                {
                    { 90, 90, 91 },
                    { 91, 91, 90 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_VacancyApplication_ApplicantId",
                table: "VacancyApplication",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_VacancyApplication_VacancyId",
                table: "VacancyApplication",
                column: "VacancyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VacancyApplication");

            migrationBuilder.DropTable(
                name: "Applicants");

            migrationBuilder.DropTable(
                name: "Vacancy");
        }
    }
}
