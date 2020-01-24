using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class UpdatedNotazrier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Notarizers");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Notarizers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Notarizers");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Notarizers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
