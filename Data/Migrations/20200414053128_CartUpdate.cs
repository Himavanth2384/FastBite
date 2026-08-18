using Microsoft.EntityFrameworkCore.Migrations;

namespace FastBite.Data.Migrations
{
    public partial class CartUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Cart",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Cart",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "Cart",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "email",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "name",
                table: "Cart");
        }
    }
}
