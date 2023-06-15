using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VinEcomDbContext.Migrations
{
    /// <inheritdoc />
    public partial class update_cloud_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Building_BuildingId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "LastWithdraw",
                table: "Store");

            migrationBuilder.AlterColumn<double>(
                name: "CommissionPercent",
                table: "Store",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "Store",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "BuildingId",
                table: "Order",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Building_BuildingId",
                table: "Order",
                column: "BuildingId",
                principalTable: "Building",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Building_BuildingId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Store");

            migrationBuilder.AlterColumn<decimal>(
                name: "CommissionPercent",
                table: "Store",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastWithdraw",
                table: "Store",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BuildingId",
                table: "Order",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Building_BuildingId",
                table: "Order",
                column: "BuildingId",
                principalTable: "Building",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
