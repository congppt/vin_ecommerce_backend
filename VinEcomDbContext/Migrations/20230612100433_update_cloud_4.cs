using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VinEcomDbContext.Migrations
{
    /// <inheritdoc />
    public partial class update_cloud_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsWorking",
                table: "Store",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsWorking",
                table: "Store");
        }
    }
}
