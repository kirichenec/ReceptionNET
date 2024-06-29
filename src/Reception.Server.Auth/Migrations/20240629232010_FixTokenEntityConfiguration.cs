using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reception.Server.Auth.Migrations
{
    /// <inheritdoc />
    public partial class FixTokenEntityConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "10000.Vu2dFkd1t3QvtvaXZm56zg==.dcrZ8CkHtpEg9rSYRu7/FNJUHDxuGeiU+4LD1vfPUg4=");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "10000.5ZAd1tkXXwpajvshEm95vQ==.jsMhLrZjMbYtHGBtpA0XYAyvGi0KucxT7+FzuqfhLnM=");
        }
    }
}
