using Microsoft.EntityFrameworkCore.Migrations;

namespace AffilateSource.Server.DataEntity.Migrations
{
    public partial class updatecategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Categories");
        }
    }
}
