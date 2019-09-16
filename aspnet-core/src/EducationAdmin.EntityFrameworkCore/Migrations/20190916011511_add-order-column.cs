using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationAdmin.Migrations
{
    public partial class addordercolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "order",
                table: "Subjects",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "order",
                table: "Options",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "order",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "order",
                table: "Options");
        }
    }
}
