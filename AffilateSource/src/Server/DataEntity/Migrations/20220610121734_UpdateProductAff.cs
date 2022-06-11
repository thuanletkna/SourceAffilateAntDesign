using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AffilateSource.Server.DataEntity.Migrations
{
    public partial class UpdateProductAff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageProducts",
                table: "PostDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkAffilateLazada",
                table: "PostDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkAffilateOther",
                table: "PostDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkAffilateShopee",
                table: "PostDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageProducts",
                table: "PostDetails");

            migrationBuilder.DropColumn(
                name: "LinkAffilateLazada",
                table: "PostDetails");

            migrationBuilder.DropColumn(
                name: "LinkAffilateOther",
                table: "PostDetails");

            migrationBuilder.DropColumn(
                name: "LinkAffilateShopee",
                table: "PostDetails");
        }
    }
}
