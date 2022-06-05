using Microsoft.EntityFrameworkCore.Migrations;

namespace AffilateSource.Server.DataEntity.Migrations
{
    public partial class DbInitialUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryParentId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImageProducts",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryParentId",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryParentId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImageProducts",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryParentId",
                table: "Posts");
        }
    }
}
