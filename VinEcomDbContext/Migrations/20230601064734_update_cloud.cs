using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VinEcomDbContext.Migrations
{
    /// <inheritdoc />
    public partial class update_cloud : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOutOfStock",
                table: "Product",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Building",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOutOfStock",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Building");
        }
    }
}
