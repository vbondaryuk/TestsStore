using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestsStore.Api.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Id = table.Column<Guid>(nullable: false, defaultValue: new Guid("a07ce5c3-8b7b-4fd6-b1e7-53ac28ed4e64"))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 300, nullable: false),
                    FullName = table.Column<string>(maxLength: 1000, nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Test_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Build",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    StatusId = table.Column<Guid>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Build", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Build_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Build_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestResult",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TestId = table.Column<Guid>(nullable: false),
                    BuildId = table.Column<Guid>(nullable: false),
                    Duration = table.Column<TimeSpan>(nullable: false),
                    StatusId = table.Column<Guid>(nullable: false),
                    Messages = table.Column<string>(maxLength: 5000, nullable: true),
                    StackTrace = table.Column<string>(maxLength: 5000, nullable: true),
                    ErrorMessage = table.Column<string>(maxLength: 5000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestResult_Build_BuildId",
                        column: x => x.BuildId,
                        principalTable: "Build",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestResult_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestResult_Test_TestId",
                        column: x => x.TestId,
                        principalTable: "Test",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Build_ProjectId",
                table: "Build",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Build_StatusId",
                table: "Build",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_Name",
                table: "Project",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Test_ProjectId",
                table: "Test",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResult_BuildId",
                table: "TestResult",
                column: "BuildId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResult_StatusId",
                table: "TestResult",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResult_TestId",
                table: "TestResult",
                column: "TestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestResult");

            migrationBuilder.DropTable(
                name: "Build");

            migrationBuilder.DropTable(
                name: "Test");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Project");
        }
    }
}
