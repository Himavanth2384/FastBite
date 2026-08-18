using Microsoft.EntityFrameworkCore.Migrations;

namespace FastBite.Data.Migrations
{
    public partial class RestaurantpartTwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RestaurantOwner",
                table: "Restaurant");

            migrationBuilder.AddColumn<string>(
                name: "OwenerID",
                table: "Restaurant",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_OwenerID",
                table: "Restaurant",
                column: "OwenerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurant_AspNetUsers_OwenerID",
                table: "Restaurant",
                column: "OwenerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurant_AspNetUsers_OwenerID",
                table: "Restaurant");

            migrationBuilder.DropIndex(
                name: "IX_Restaurant_OwenerID",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "OwenerID",
                table: "Restaurant");

            migrationBuilder.AddColumn<string>(
                name: "RestaurantOwner",
                table: "Restaurant",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
