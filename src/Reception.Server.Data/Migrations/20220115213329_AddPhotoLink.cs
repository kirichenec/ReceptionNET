using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reception.Server.Data.Migrations
{
    public partial class AddPhotoLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdditionalInfoId",
                schema: "Person",
                table: "Person",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PersonAdditional",
                schema: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PhotoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonAdditional", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Person_AdditionalInfoId",
                schema: "Person",
                table: "Person",
                column: "AdditionalInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_PersonAdditional_AdditionalInfoId",
                schema: "Person",
                table: "Person",
                column: "AdditionalInfoId",
                principalSchema: "Person",
                principalTable: "PersonAdditional",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_PersonAdditional_AdditionalInfoId",
                schema: "Person",
                table: "Person");

            migrationBuilder.DropTable(
                name: "PersonAdditional",
                schema: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_AdditionalInfoId",
                schema: "Person",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "AdditionalInfoId",
                schema: "Person",
                table: "Person");
        }
    }
}
