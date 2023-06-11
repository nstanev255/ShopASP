using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopASP.Migrations
{
    /// <inheritdoc />
    public partial class OrderMigrationUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_UUID",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UUID",
                table: "Orders",
                column: "UUID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_UUID",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UUID",
                table: "Orders",
                column: "UUID");
        }
    }
}
