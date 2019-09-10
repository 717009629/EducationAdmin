using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationAdmin.Migrations
{
    public partial class studentphone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Students",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Students",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
