using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationAdmin.Migrations
{
    public partial class @class : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Orders_OrderId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Students_StudentId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_OrderId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Lessons");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Lessons",
                newName: "ClassId");

            migrationBuilder.RenameIndex(
                name: "IX_Lessons_StudentId",
                table: "Lessons",
                newName: "IX_Lessons_ClassId");

            migrationBuilder.AddColumn<long>(
                name: "ClassId",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClassType",
                table: "Courses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ClassType = table.Column<int>(nullable: false),
                    CourseId = table.Column<long>(nullable: false),
                    TeacherId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classes_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Classes_AbpUsers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClassId",
                table: "Orders",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_CourseId",
                table: "Classes",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_TeacherId",
                table: "Classes",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Classes_ClassId",
                table: "Lessons",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Classes_ClassId",
                table: "Orders",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Classes_ClassId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Classes_ClassId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ClassId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ClassType",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "ClassId",
                table: "Lessons",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Lessons_ClassId",
                table: "Lessons",
                newName: "IX_Lessons_StudentId");

            migrationBuilder.AddColumn<long>(
                name: "OrderId",
                table: "Lessons",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_OrderId",
                table: "Lessons",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Orders_OrderId",
                table: "Lessons",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Students_StudentId",
                table: "Lessons",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
