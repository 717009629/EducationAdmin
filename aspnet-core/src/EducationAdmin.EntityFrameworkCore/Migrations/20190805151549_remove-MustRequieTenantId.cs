using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationAdmin.Migrations
{
    public partial class removeMustRequieTenantId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Students");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Students",
                nullable: false,
                defaultValue: 0);
        }
    }
}
