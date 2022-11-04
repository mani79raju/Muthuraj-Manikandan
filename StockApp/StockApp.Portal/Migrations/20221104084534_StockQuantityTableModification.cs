using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockApp.Portal.Migrations
{
    public partial class StockQuantityTableModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StocksQuantity_Stocks_StocksStockId",
                table: "StocksQuantity");

            migrationBuilder.DropIndex(
                name: "IX_StocksQuantity_StocksStockId",
                table: "StocksQuantity");

            migrationBuilder.DropColumn(
                name: "StocksStockId",
                table: "StocksQuantity");

            migrationBuilder.AlterColumn<string>(
                name: "StockId",
                table: "StocksQuantity",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_StocksQuantity_StockId",
                table: "StocksQuantity",
                column: "StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_StocksQuantity_Stocks_StockId",
                table: "StocksQuantity",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "StockId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StocksQuantity_Stocks_StockId",
                table: "StocksQuantity");

            migrationBuilder.DropIndex(
                name: "IX_StocksQuantity_StockId",
                table: "StocksQuantity");

            migrationBuilder.AlterColumn<string>(
                name: "StockId",
                table: "StocksQuantity",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "StocksStockId",
                table: "StocksQuantity",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StocksQuantity_StocksStockId",
                table: "StocksQuantity",
                column: "StocksStockId");

            migrationBuilder.AddForeignKey(
                name: "FK_StocksQuantity_Stocks_StocksStockId",
                table: "StocksQuantity",
                column: "StocksStockId",
                principalTable: "Stocks",
                principalColumn: "StockId");
        }
    }
}
