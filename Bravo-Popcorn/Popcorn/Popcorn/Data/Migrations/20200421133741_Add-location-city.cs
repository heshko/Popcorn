using Microsoft.EntityFrameworkCore.Migrations;

namespace Popcorn.Data.Migrations
{
    public partial class Addlocationcity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "city",
                table: "Cinemas");

            migrationBuilder.AddColumn<int>(
                name: "cityId",
                table: "Cinemas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cinemas_cityId",
                table: "Cinemas",
                column: "cityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cinemas_Cities_cityId",
                table: "Cinemas",
                column: "cityId",
                principalTable: "Cities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cinemas_Cities_cityId",
                table: "Cinemas");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cinemas_cityId",
                table: "Cinemas");

            migrationBuilder.DropColumn(
                name: "cityId",
                table: "Cinemas");

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "Cinemas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
