using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class QualificationDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotarizerQualificationDocument_Notarizers_NotarizerId",
                table: "NotarizerQualificationDocument");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotarizerQualificationDocument",
                table: "NotarizerQualificationDocument");

            migrationBuilder.RenameTable(
                name: "NotarizerQualificationDocument",
                newName: "QualificationDocuments");

            migrationBuilder.RenameIndex(
                name: "IX_NotarizerQualificationDocument_NotarizerId",
                table: "QualificationDocuments",
                newName: "IX_QualificationDocuments_NotarizerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QualificationDocuments",
                table: "QualificationDocuments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QualificationDocuments_Notarizers_NotarizerId",
                table: "QualificationDocuments",
                column: "NotarizerId",
                principalTable: "Notarizers",
                principalColumn: "NotarizerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QualificationDocuments_Notarizers_NotarizerId",
                table: "QualificationDocuments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QualificationDocuments",
                table: "QualificationDocuments");

            migrationBuilder.RenameTable(
                name: "QualificationDocuments",
                newName: "NotarizerQualificationDocument");

            migrationBuilder.RenameIndex(
                name: "IX_QualificationDocuments_NotarizerId",
                table: "NotarizerQualificationDocument",
                newName: "IX_NotarizerQualificationDocument_NotarizerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotarizerQualificationDocument",
                table: "NotarizerQualificationDocument",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NotarizerQualificationDocument_Notarizers_NotarizerId",
                table: "NotarizerQualificationDocument",
                column: "NotarizerId",
                principalTable: "Notarizers",
                principalColumn: "NotarizerId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
