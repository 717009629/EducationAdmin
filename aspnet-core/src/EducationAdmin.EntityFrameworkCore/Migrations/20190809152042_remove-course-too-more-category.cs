using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationAdmin.Migrations
{
    public partial class removecoursetoomorecategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LargeCategory",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "SubCategory",
                table: "Courses",
                newName: "Category");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Courses",
                newName: "SubCategory");

            migrationBuilder.AddColumn<string>(
                name: "LargeCategory",
                table: "Courses",
                nullable: true);
        }
    }
}
