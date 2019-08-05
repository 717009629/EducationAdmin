using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationAdmin.Migrations
{
    public partial class AddStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Sex = table.Column<bool>(nullable: true),
                    Parent = table.Column<string>(nullable: true),
                    Relation = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    FatherPhone = table.Column<string>(nullable: true),
                    MotherPhone = table.Column<string>(nullable: true),
                    School = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: true),
                    Grade = table.Column<string>(nullable: true),
                    CourseType = table.Column<string>(nullable: true),
                    StudentType = table.Column<string>(nullable: true),
                    TeachMethod = table.Column<string>(nullable: true),
                    Origin = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    SalesmanId = table.Column<long>(nullable: false),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_AbpUsers_SalesmanId",
                        column: x => x.SalesmanId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_SalesmanId",
                table: "Students",
                column: "SalesmanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
