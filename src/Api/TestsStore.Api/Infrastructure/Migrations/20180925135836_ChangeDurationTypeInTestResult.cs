using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestsStore.Api.Infrastructure.Migrations
{
	public partial class ChangeDurationTypeInTestResult : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "Duration",
				table: "TestResult");

			migrationBuilder.AddColumn<int>(
				name: "Duration",
				table: "TestResult",
				nullable: false,
				defaultValue: 0);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "Duration",
				table: "TestResult");

			migrationBuilder.AddColumn<TimeSpan>(
				name: "Duration",
				table: "TestResult",
				nullable: false,
				defaultValue: TimeSpan.FromSeconds(0));
		}
	}
}
