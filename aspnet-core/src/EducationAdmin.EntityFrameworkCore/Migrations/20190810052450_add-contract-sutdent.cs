using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationAdmin.Migrations
{
    public partial class addcontractsutdent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "StudentId",
                table: "Contracts",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_StudentId",
                table: "Contracts",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Students_StudentId",
                table: "Contracts",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Students_StudentId",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_StudentId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Contracts");
        }
    }
}
