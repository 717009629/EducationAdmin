using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationAdmin.Migrations
{
    public partial class lessonattendance2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonAttendances_Students_StudentId",
                table: "LessonAttendances");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "LessonAttendances",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_LessonAttendances_StudentId",
                table: "LessonAttendances",
                newName: "IX_LessonAttendances_OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonAttendances_Orders_OrderId",
                table: "LessonAttendances",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonAttendances_Orders_OrderId",
                table: "LessonAttendances");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "LessonAttendances",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_LessonAttendances_OrderId",
                table: "LessonAttendances",
                newName: "IX_LessonAttendances_StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonAttendances_Students_StudentId",
                table: "LessonAttendances",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
