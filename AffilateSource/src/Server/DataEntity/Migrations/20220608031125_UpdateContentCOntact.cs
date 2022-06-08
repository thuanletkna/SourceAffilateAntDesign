using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AffilateSource.Server.DataEntity.Migrations
{
    public partial class UpdateContentCOntact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentHome",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentHome",
                table: "Contacts");
        }
    }
}
