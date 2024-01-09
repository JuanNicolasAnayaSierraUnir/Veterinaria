using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFriend.Server.Migrations
{
    public partial class AjusteModeloCiudad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cities_CitiesModelId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CitiesModelId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CitiesModelId",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CitiesModelId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CitiesModelId",
                table: "Users",
                column: "CitiesModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cities_CitiesModelId",
                table: "Users",
                column: "CitiesModelId",
                principalTable: "Cities",
                principalColumn: "Id");
        }
    }
}
