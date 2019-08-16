using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationAdmin.Migrations
{
    public partial class spellError : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Courses_CourseId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "AuditedReson",
                table: "Contracts",
                newName: "AuditedReason");

            migrationBuilder.AlterColumn<long>(
                name: "CourseId",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Courses_CourseId",
                table: "Orders",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Courses_CourseId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "AuditedReason",
                table: "Contracts",
                newName: "AuditedReson");

            migrationBuilder.AlterColumn<long>(
                name: "CourseId",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Courses_CourseId",
                table: "Orders",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
