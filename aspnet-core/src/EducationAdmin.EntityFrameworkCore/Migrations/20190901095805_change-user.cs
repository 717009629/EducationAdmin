using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationAdmin.Migrations
{
    public partial class changeuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EducationBackground",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityCard",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaritalStatus",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nation",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PoliticsStatus",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SchoolTag",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkingYears",
                table: "AbpUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "District",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "EducationBackground",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "IdentityCard",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "MaritalStatus",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Nation",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "PoliticsStatus",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "SchoolTag",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "WorkingYears",
                table: "AbpUsers");
        }
    }
}
