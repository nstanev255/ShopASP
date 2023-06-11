using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopASP.Migrations
{
    /// <inheritdoc />
    public partial class OrderMigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UUID",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UUID",
                table: "Orders",
                column: "UUID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_UUID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UUID",
                table: "Orders");
        }
    }
}
