using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationAdmin.Migrations
{
    public partial class studentaddfathermother : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Father",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mother",
                table: "Students",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Father",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Mother",
                table: "Students");
        }
    }
}
