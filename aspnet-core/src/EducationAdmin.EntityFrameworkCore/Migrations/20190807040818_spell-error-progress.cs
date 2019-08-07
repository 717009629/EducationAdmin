using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationAdmin.Migrations
{
    public partial class spellerrorprogress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Progess",
                table: "Records",
                newName: "Progress");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Progress",
                table: "Records",
                newName: "Progess");
        }
    }
}
