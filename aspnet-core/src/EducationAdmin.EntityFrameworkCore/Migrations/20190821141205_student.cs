using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationAdmin.Migrations
{
    public partial class student : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_AbpUsers_SalesmanId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "SalesmanId",
                table: "Students",
                newName: "TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_SalesmanId",
                table: "Students",
                newName: "IX_Students_TeacherId");

            migrationBuilder.AddColumn<double>(
                name: "Biology",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Chemistry",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Chinese",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClassRank",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "English",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Geography",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GradeRank",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "History",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Math",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Physics",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Political",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrivateLesson",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SchoolBegin",
                table: "Students",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AbpUsers_TeacherId",
                table: "Students",
                column: "TeacherId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_AbpUsers_TeacherId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Biology",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Chemistry",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Chinese",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ClassRank",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "English",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Geography",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "GradeRank",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "History",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Math",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Physics",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Political",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PrivateLesson",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SchoolBegin",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "Students",
                newName: "SalesmanId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_TeacherId",
                table: "Students",
                newName: "IX_Students_SalesmanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AbpUsers_SalesmanId",
                table: "Students",
                column: "SalesmanId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
