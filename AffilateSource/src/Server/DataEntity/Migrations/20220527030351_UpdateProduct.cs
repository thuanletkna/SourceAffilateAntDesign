using Microsoft.EntityFrameworkCore.Migrations;

namespace AffilateSource.Server.DataEntity.Migrations
{
    public partial class UpdateProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LinkAffilate",
                table: "Products",
                newName: "LinkAffilateTiki");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Products",
                newName: "LinkAffilateShopee");

            migrationBuilder.AddColumn<string>(
                name: "LinkAffilateLazada",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkAffilateOther",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkAffilateLazada",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LinkAffilateOther",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "LinkAffilateTiki",
                table: "Products",
                newName: "LinkAffilate");

            migrationBuilder.RenameColumn(
                name: "LinkAffilateShopee",
                table: "Products",
                newName: "Image");
        }
    }
}
