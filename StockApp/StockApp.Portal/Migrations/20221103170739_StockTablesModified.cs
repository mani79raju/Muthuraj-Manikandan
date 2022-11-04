using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockApp.Portal.Migrations
{
    public partial class StockTablesModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_StocksQuantity_StockQuantityId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_StockTransactions_StockTransactionsStockTransactionId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_StockQuantityId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_StockTransactionsStockTransactionId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "StockQuantityId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "StockTransactionsStockTransactionId",
                table: "Stocks");

            migrationBuilder.RenameColumn(
                name: "MarketPrice",
                table: "StockTransactions",
                newName: "SellPrice");

            migrationBuilder.AddColumn<double>(
                name: "BuyPrice",
                table: "StockTransactions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "StocksStockId",
                table: "StockTransactions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StocksStockId",
                table: "StocksQuantity",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactions_StocksStockId",
                table: "StockTransactions",
                column: "StocksStockId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactions_Stocks_StocksStockId",
                table: "StockTransactions",
                column: "StocksStockId",
                principalTable: "Stocks",
                principalColumn: "StockId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StocksQuantity_Stocks_StocksStockId",
                table: "StocksQuantity");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactions_Stocks_StocksStockId",
                table: "StockTransactions");

            migrationBuilder.DropIndex(
                name: "IX_StockTransactions_StocksStockId",
                table: "StockTransactions");

            migrationBuilder.DropIndex(
                name: "IX_StocksQuantity_StocksStockId",
                table: "StocksQuantity");

            migrationBuilder.DropColumn(
                name: "BuyPrice",
                table: "StockTransactions");

            migrationBuilder.DropColumn(
                name: "StocksStockId",
                table: "StockTransactions");

            migrationBuilder.DropColumn(
                name: "StocksStockId",
                table: "StocksQuantity");

            migrationBuilder.RenameColumn(
                name: "SellPrice",
                table: "StockTransactions",
                newName: "MarketPrice");

            migrationBuilder.AddColumn<string>(
                name: "StockQuantityId",
                table: "Stocks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StockTransactionsStockTransactionId",
                table: "Stocks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_StockQuantityId",
                table: "Stocks",
                column: "StockQuantityId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_StockTransactionsStockTransactionId",
                table: "Stocks",
                column: "StockTransactionsStockTransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_StocksQuantity_StockQuantityId",
                table: "Stocks",
                column: "StockQuantityId",
                principalTable: "StocksQuantity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_StockTransactions_StockTransactionsStockTransactionId",
                table: "Stocks",
                column: "StockTransactionsStockTransactionId",
                principalTable: "StockTransactions",
                principalColumn: "StockTransactionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
