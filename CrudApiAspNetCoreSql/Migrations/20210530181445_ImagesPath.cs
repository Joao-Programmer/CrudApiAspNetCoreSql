using Microsoft.EntityFrameworkCore.Migrations;

namespace CrudApiAspNetCoreSql.Migrations
{
    public partial class ImagesPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryUrl",
                table: "Categories",
                newName: "CategoryImagePath");

            migrationBuilder.AddColumn<string>(
                name: "MenuItemImagePath",
                table: "MenuItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MenuItemImagePath",
                table: "MenuItems");

            migrationBuilder.RenameColumn(
                name: "CategoryImagePath",
                table: "Categories",
                newName: "CategoryUrl");
        }
    }
}
