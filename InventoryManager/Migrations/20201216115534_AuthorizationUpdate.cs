using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryManager.Migrations
{
    public partial class AuthorizationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RedirectUrl",
                table: "NopComAuthorization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServerUrl",
                table: "NopComAuthorization",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RedirectUrl",
                table: "NopComAuthorization");

            migrationBuilder.DropColumn(
                name: "ServerUrl",
                table: "NopComAuthorization");
        }
    }
}
