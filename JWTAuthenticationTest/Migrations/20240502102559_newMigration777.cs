using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWTAuthenticationTest.Migrations
{
    /// <inheritdoc />
    public partial class newMigration777 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoleIds",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleIds",
                table: "Users");
        }
    }
}
