using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddedAgreements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "CityId",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "CourtId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateApproved",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IssuedStamp",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NotarizerId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NotarizerJudicialNumber",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Stamp",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "Status",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AgreementType",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    TemplateName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgreementType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgreementTypes",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    TemplateName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgreementTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courts",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    DocumentId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentBody = table.Column<byte[]>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.DocumentId);
                });

            migrationBuilder.CreateTable(
                name: "NotarizerQualificationDocument",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentName = table.Column<string>(nullable: true),
                    DocumentBody = table.Column<byte[]>(nullable: true),
                    DateUploaded = table.Column<DateTime>(nullable: false),
                    Verified = table.Column<bool>(nullable: false),
                    NotarizerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotarizerQualificationDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotarizerQualificationDocument_AspNetUsers_NotarizerId",
                        column: x => x.NotarizerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    IdentificationId = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QualificationDocumentLookup",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualificationDocumentLookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Agreements",
                columns: table => new
                {
                    AgreementId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hour = table.Column<string>(nullable: true),
                    Day = table.Column<string>(nullable: true),
                    AgreementDate = table.Column<DateTime>(nullable: false),
                    WakeelName = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    MuaKalName = table.Column<string>(nullable: true),
                    DateCompleted = table.Column<DateTime>(nullable: true),
                    Status = table.Column<short>(nullable: false),
                    AgreementTypeId = table.Column<short>(nullable: false),
                    DocumentId = table.Column<long>(nullable: false),
                    NotarizerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agreements", x => x.AgreementId);
                    table.ForeignKey(
                        name: "FK_Agreements_AgreementType_AgreementTypeId",
                        column: x => x.AgreementTypeId,
                        principalTable: "AgreementType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Agreements_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "DocumentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Agreements_AspNetUsers_NotarizerId",
                        column: x => x.NotarizerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleSalesContracts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerNameId = table.Column<int>(nullable: true),
                    BuyerNameId = table.Column<int>(nullable: true),
                    VehicleName = table.Column<string>(nullable: true),
                    VehicleModel = table.Column<string>(nullable: true),
                    VehicleYear = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    PlateNo = table.Column<string>(nullable: true),
                    PlateIssuingAgency = table.Column<string>(nullable: true),
                    VINBumber = table.Column<string>(nullable: true),
                    EngineNumber = table.Column<string>(nullable: true),
                    ManfacturingCountry = table.Column<string>(nullable: true),
                    ManfacturingDate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleSalesContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleSalesContracts_Person_BuyerNameId",
                        column: x => x.BuyerNameId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleSalesContracts_Person_SellerNameId",
                        column: x => x.SellerNameId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CityId",
                table: "AspNetUsers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CourtId",
                table: "AspNetUsers",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_Agreements_AgreementTypeId",
                table: "Agreements",
                column: "AgreementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Agreements_DocumentId",
                table: "Agreements",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Agreements_NotarizerId",
                table: "Agreements",
                column: "NotarizerId");

            migrationBuilder.CreateIndex(
                name: "IX_NotarizerQualificationDocument_NotarizerId",
                table: "NotarizerQualificationDocument",
                column: "NotarizerId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleSalesContracts_BuyerNameId",
                table: "VehicleSalesContracts",
                column: "BuyerNameId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleSalesContracts_SellerNameId",
                table: "VehicleSalesContracts",
                column: "SellerNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cities_CityId",
                table: "AspNetUsers",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Courts_CourtId",
                table: "AspNetUsers",
                column: "CourtId",
                principalTable: "Courts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cities_CityId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Courts_CourtId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Agreements");

            migrationBuilder.DropTable(
                name: "AgreementTypes");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Courts");

            migrationBuilder.DropTable(
                name: "NotarizerQualificationDocument");

            migrationBuilder.DropTable(
                name: "QualificationDocumentLookup");

            migrationBuilder.DropTable(
                name: "VehicleSalesContracts");

            migrationBuilder.DropTable(
                name: "AgreementType");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Person");

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
                name: "NotarizerId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NotarizerJudicialNumber",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Phone",
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
        }
    }
}
