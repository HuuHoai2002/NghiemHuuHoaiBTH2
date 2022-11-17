using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NghiemHuuHoaiBTH2.Migrations
{
    public partial class Create_Foreignkey_Student : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FacultyID",
                table: "Students",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Students_FacultyID",
                table: "Students",
                column: "FacultyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Faculties_FacultyID",
                table: "Students",
                column: "FacultyID",
                principalTable: "Faculties",
                principalColumn: "FacultyID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Faculties_FacultyID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_FacultyID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "FacultyID",
                table: "Students");
        }
    }
}
