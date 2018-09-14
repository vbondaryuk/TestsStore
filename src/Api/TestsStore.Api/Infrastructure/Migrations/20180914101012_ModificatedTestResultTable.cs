using Microsoft.EntityFrameworkCore.Migrations;

namespace TestsStore.Api.Infrastructure.Migrations
{
    public partial class ModificatedTestResultTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Test",
                newName: "ClassName");

            migrationBuilder.CreateIndex(
                name: "IX_Test_ClassName_Name",
                table: "Test",
                columns: new[] { "ClassName", "Name" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Test_ClassName_Name",
                table: "Test");

            migrationBuilder.RenameColumn(
                name: "ClassName",
                table: "Test",
                newName: "FullName");
        }
    }
}
