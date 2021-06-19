using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CrudApiAspNetCoreSql.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategorySpecialInstructions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryCreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    MenuItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuItemLargePortionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuItemPriceLarge = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MenuItemPriceSmall = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MenuItemShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuItemSmallPortionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuItemCategoryIdFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.MenuItemID);
                    table.ForeignKey(
                        name: "FK_MenuItems_Categories_MenuItemCategoryIdFk",
                        column: x => x.MenuItemCategoryIdFk,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_MenuItemCategoryIdFk",
                table: "MenuItems",
                column: "MenuItemCategoryIdFk");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
