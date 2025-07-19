using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoSoft.InventorySystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class LastLowStockWarningDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastLowStockWarningDate",
                table: "Products",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastLowStockWarningDate",
                table: "Products");
        }
    }
}
