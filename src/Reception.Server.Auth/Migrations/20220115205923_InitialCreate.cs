using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reception.Server.Auth.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Auth");

            migrationBuilder.CreateTable(
                name: "Token",
                schema: "Auth",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Value = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Token", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Auth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Login = table.Column<string>(type: "TEXT", nullable: false),
                    MiddleName = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.UniqueConstraint("IX_Login", x => x.Login);
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "User",
                columns: new[] { "Id", "FirstName", "LastName", "Login", "MiddleName", "Password" },
                values: new object[] { 1, null, null, "admin", null, "10000.5ZAd1tkXXwpajvshEm95vQ==.jsMhLrZjMbYtHGBtpA0XYAyvGi0KucxT7+FzuqfhLnM=" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Token",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Auth");
        }
    }
}
