using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopASP.Migrations
{
    /// <inheritdoc />
    public partial class Props : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FrontCoverId",
                table: "Products",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Images",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_FrontCoverId",
                table: "Products",
                column: "FrontCoverId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProductId",
                table: "Images",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Products_ProductId",
                table: "Images",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Images_FrontCoverId",
                table: "Products",
                column: "FrontCoverId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Products_ProductId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Images_FrontCoverId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_FrontCoverId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Images_ProductId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "FrontCoverId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Images");
        }
    }
}
