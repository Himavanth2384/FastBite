using Microsoft.EntityFrameworkCore.Migrations;

namespace FastBite.Data.Migrations
{
    public partial class CartUpdateparttwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "mobilenumber",
                table: "Cart",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "mobilenumber",
                table: "Cart");
        }
    }
}
