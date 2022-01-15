using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reception.Server.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Person");

            migrationBuilder.CreateTable(
                name: "Post",
                schema: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Comment = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                schema: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Comment = table.Column<string>(type: "TEXT", nullable: true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    MiddleName = table.Column<string>(type: "TEXT", nullable: true),
                    PostId = table.Column<int>(type: "INTEGER", nullable: true),
                    SecondName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Person_Post_PostId",
                        column: x => x.PostId,
                        principalSchema: "Person",
                        principalTable: "Post",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "Person",
                table: "Person",
                columns: new[] { "Id", "Comment", "FirstName", "MiddleName", "PostId", "SecondName" },
                values: new object[] { 1, null, "Igor", "Grigorievich", null, "Kirichenko" });

            migrationBuilder.InsertData(
                schema: "Person",
                table: "Person",
                columns: new[] { "Id", "Comment", "FirstName", "MiddleName", "PostId", "SecondName" },
                values: new object[] { 2, null, "Anna", "Sergeevna", null, "Ushkalova" });

            migrationBuilder.InsertData(
                schema: "Person",
                table: "Post",
                columns: new[] { "Id", "Comment", "Name" },
                values: new object[] { 1, null, "Brainfucker" });

            migrationBuilder.CreateIndex(
                name: "IX_Person_PostId",
                schema: "Person",
                table: "Person",
                column: "PostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Person",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "Post",
                schema: "Person");
        }
    }
}
