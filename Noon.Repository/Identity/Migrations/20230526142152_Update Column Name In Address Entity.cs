using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Noon.Repository.Identity.Migrations
{
    public partial class UpdateColumnNameInAddressEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cuntry",
                table: "Address",
                newName: "Country");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Address",
                newName: "Cuntry");
        }
    }
}
