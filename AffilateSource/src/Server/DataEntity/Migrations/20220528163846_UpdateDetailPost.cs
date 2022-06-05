using Microsoft.EntityFrameworkCore.Migrations;

namespace AffilateSource.Server.DataEntity.Migrations
{
    public partial class UpdateDetailPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TitleDetail",
                table: "PostDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TitleDetail",
                table: "PostDetails");
        }
    }
}
