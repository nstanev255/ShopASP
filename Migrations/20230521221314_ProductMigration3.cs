using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShopASP.Migrations
{
    /// <inheritdoc />
    public partial class ProductMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemRequirement_Products_ProductId",
                table: "SystemRequirement");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemRequirement_Products_ProductId1",
                table: "SystemRequirement");

            migrationBuilder.DropIndex(
                name: "IX_SystemRequirement_ProductId",
                table: "SystemRequirement");

            migrationBuilder.DropIndex(
                name: "IX_SystemRequirement_ProductId1",
                table: "SystemRequirement");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "SystemRequirement");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "SystemRequirement");

            migrationBuilder.CreateTable(
                name: "ProductMinimalSystemRequirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    SystemRequirementId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMinimalSystemRequirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductMinimalSystemRequirements_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductMinimalSystemRequirements_SystemRequirement_SystemRe~",
                        column: x => x.SystemRequirementId,
                        principalTable: "SystemRequirement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductRecommendedSystemRequirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    SystemRequirementId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRecommendedSystemRequirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductRecommendedSystemRequirements_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductRecommendedSystemRequirements_SystemRequirement_Syst~",
                        column: x => x.SystemRequirementId,
                        principalTable: "SystemRequirement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductMinimalSystemRequirements_ProductId",
                table: "ProductMinimalSystemRequirements",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMinimalSystemRequirements_SystemRequirementId",
                table: "ProductMinimalSystemRequirements",
                column: "SystemRequirementId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRecommendedSystemRequirements_ProductId",
                table: "ProductRecommendedSystemRequirements",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRecommendedSystemRequirements_SystemRequirementId",
                table: "ProductRecommendedSystemRequirements",
                column: "SystemRequirementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductMinimalSystemRequirements");

            migrationBuilder.DropTable(
                name: "ProductRecommendedSystemRequirements");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "SystemRequirement",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "SystemRequirement",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemRequirement_ProductId",
                table: "SystemRequirement",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemRequirement_ProductId1",
                table: "SystemRequirement",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemRequirement_Products_ProductId",
                table: "SystemRequirement",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemRequirement_Products_ProductId1",
                table: "SystemRequirement",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
