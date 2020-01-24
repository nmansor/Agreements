using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddedNotarizations4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agreements_AspNetUsers_NotarizerId",
                table: "Agreements");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cities_CityId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Courts_CourtId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_NotarizerQualificationDocument_AspNetUsers_NotarizerId",
                table: "NotarizerQualificationDocument");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CityId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CourtId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CourtId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateApproved",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IssuedStamp",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LandPhone",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NotarizerId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NotarizerJudicialNumber",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Stamp",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FamilyName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FatherName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MobilePhone",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Notarizers",
                columns: table => new
                {
                    NotarizerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    LandPhone = table.Column<string>(nullable: true),
                    NotarizerJudicialNumber = table.Column<string>(nullable: true),
                    CityId = table.Column<short>(maxLength: 100, nullable: true),
                    CourtId = table.Column<short>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    FatherName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    FamilyName = table.Column<string>(nullable: true),
                    MobilePhone = table.Column<string>(nullable: true),
                    Status = table.Column<short>(nullable: false),
                    IssuedStamp = table.Column<bool>(nullable: false),
                    DateApproved = table.Column<DateTime>(nullable: true),
                    Stamp = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notarizers", x => x.NotarizerId);
                    table.ForeignKey(
                        name: "FK_Notarizers_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notarizers_Courts_CourtId",
                        column: x => x.CourtId,
                        principalTable: "Courts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notarizers_CityId",
                table: "Notarizers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Notarizers_CourtId",
                table: "Notarizers",
                column: "CourtId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Agreements_Notarizers_NotarizerId",
            //    table: "Agreements",
            //    column: "NotarizerId",
            //    principalTable: "Notarizers",
            //    principalColumn: "NotarizerId",
            //    onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotarizerQualificationDocument_Notarizers_NotarizerId",
                table: "NotarizerQualificationDocument",
                column: "NotarizerId",
                principalTable: "Notarizers",
                principalColumn: "NotarizerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Agreements_Notarizers_NotarizerId",
            //    table: "Agreements");

            migrationBuilder.DropForeignKey(
                name: "FK_NotarizerQualificationDocument_Notarizers_NotarizerId",
                table: "NotarizerQualificationDocument");

            migrationBuilder.DropTable(
                name: "Notarizers");

            migrationBuilder.AddColumn<short>(
                name: "CityId",
                table: "AspNetUsers",
                type: "smallint",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "CourtId",
                table: "AspNetUsers",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateApproved",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IssuedStamp",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LandPhone",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NotarizerId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NotarizerJudicialNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Stamp",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "Status",
                table: "AspNetUsers",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FamilyName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FatherName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobilePhone",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CityId",
                table: "AspNetUsers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CourtId",
                table: "AspNetUsers",
                column: "CourtId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agreements_AspNetUsers_NotarizerId",
                table: "Agreements",
                column: "NotarizerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cities_CityId",
                table: "AspNetUsers",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Courts_CourtId",
                table: "AspNetUsers",
                column: "CourtId",
                principalTable: "Courts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NotarizerQualificationDocument_AspNetUsers_NotarizerId",
                table: "NotarizerQualificationDocument",
                column: "NotarizerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
