using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AffilateSource.Server.DataEntity.Migrations
{
    public partial class UpdateProductAffilate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductAffilateName",
                table: "PostDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductAffilatePrice",
                table: "PostDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductAffilateName",
                table: "PostDetails");

            migrationBuilder.DropColumn(
                name: "ProductAffilatePrice",
                table: "PostDetails");
        }
    }
}
