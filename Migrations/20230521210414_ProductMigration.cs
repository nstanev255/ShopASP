using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShopASP.Migrations
{
    /// <inheritdoc />
    public partial class ProductMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeveloperId",
                table: "Products",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlatformId",
                table: "Products",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ReleaseDate",
                table: "Products",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Developer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Platform",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LogoUrl = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platform", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_DeveloperId",
                table: "Products",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PlatformId",
                table: "Products",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ProductId",
                table: "Category",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Developer_DeveloperId",
                table: "Products",
                column: "DeveloperId",
                principalTable: "Developer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Platform_PlatformId",
                table: "Products",
                column: "PlatformId",
                principalTable: "Platform",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Developer_DeveloperId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Platform_PlatformId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Developer");

            migrationBuilder.DropTable(
                name: "Platform");

            migrationBuilder.DropIndex(
                name: "IX_Products_DeveloperId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_PlatformId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DeveloperId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PlatformId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Products");
        }
    }
}
