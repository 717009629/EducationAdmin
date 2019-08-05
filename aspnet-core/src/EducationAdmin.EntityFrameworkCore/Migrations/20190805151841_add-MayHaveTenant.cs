using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationAdmin.Migrations
{
    public partial class addMayHaveTenant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Students",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Students");
        }
    }
}
