using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VinEcomDbContext.Migrations
{
    /// <inheritdoc />
    public partial class update_cloud_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Shipper",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Shipper");
        }
    }
}
