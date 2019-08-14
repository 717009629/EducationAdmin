using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationAdmin.Migrations
{
    public partial class spellError : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OhrerGuadianPhone",
                table: "Customer",
                newName: "OtherGuadianPhone");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OtherGuadianPhone",
                table: "Customer",
                newName: "OhrerGuadianPhone");
        }
    }
}
