using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VinEcomDbContext.Migrations
{
    /// <inheritdoc />
    public partial class update_cloud_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StoreStaff_UserId",
                table: "StoreStaff");

            migrationBuilder.DropIndex(
                name: "IX_Shipper_UserId",
                table: "Shipper");

            migrationBuilder.DropIndex(
                name: "IX_Customer_UserId",
                table: "Customer");

            migrationBuilder.CreateIndex(
                name: "IX_StoreStaff_UserId",
                table: "StoreStaff",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shipper_UserId",
                table: "Shipper",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_UserId",
                table: "Customer",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StoreStaff_UserId",
                table: "StoreStaff");

            migrationBuilder.DropIndex(
                name: "IX_Shipper_UserId",
                table: "Shipper");

            migrationBuilder.DropIndex(
                name: "IX_Customer_UserId",
                table: "Customer");

            migrationBuilder.CreateIndex(
                name: "IX_StoreStaff_UserId",
                table: "StoreStaff",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipper_UserId",
                table: "Shipper",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_UserId",
                table: "Customer",
                column: "UserId");
        }
    }
}
