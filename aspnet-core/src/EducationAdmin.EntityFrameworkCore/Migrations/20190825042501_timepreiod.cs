using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationAdmin.Migrations
{
    public partial class timepreiod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LessonNumber",
                table: "Lessons");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "End",
                table: "Lessons",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Start",
                table: "Lessons",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<long>(
                name: "TimePeriodId",
                table: "Lessons",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "TimePeriods",
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
                    Start = table.Column<TimeSpan>(nullable: false),
                    End = table.Column<TimeSpan>(nullable: false),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimePeriods", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_TimePeriodId",
                table: "Lessons",
                column: "TimePeriodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_TimePeriods_TimePeriodId",
                table: "Lessons",
                column: "TimePeriodId",
                principalTable: "TimePeriods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_TimePeriods_TimePeriodId",
                table: "Lessons");

            migrationBuilder.DropTable(
                name: "TimePeriods");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_TimePeriodId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "End",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Start",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "TimePeriodId",
                table: "Lessons");

            migrationBuilder.AddColumn<int>(
                name: "LessonNumber",
                table: "Lessons",
                nullable: false,
                defaultValue: 0);
        }
    }
}
