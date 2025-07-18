using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoSoft.InventorySystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddingAlertThresholdQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AlertThresholdQuantity",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlertThresholdQuantity",
                table: "Products");
        }
    }
}
