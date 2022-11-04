using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockApp.Portal.Migrations
{
    public partial class StockTransactionTableKeysModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactions_Stocks_StocksStockId",
                table: "StockTransactions");

            migrationBuilder.DropIndex(
                name: "IX_StockTransactions_StocksStockId",
                table: "StockTransactions");

            migrationBuilder.DropColumn(
                name: "StocksStockId",
                table: "StockTransactions");

            migrationBuilder.AlterColumn<string>(
                name: "StockId",
                table: "StockTransactions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactions_StockId",
                table: "StockTransactions",
                column: "StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactions_Stocks_StockId",
                table: "StockTransactions",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "StockId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactions_Stocks_StockId",
                table: "StockTransactions");

            migrationBuilder.DropIndex(
                name: "IX_StockTransactions_StockId",
                table: "StockTransactions");

            migrationBuilder.AlterColumn<string>(
                name: "StockId",
                table: "StockTransactions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "StocksStockId",
                table: "StockTransactions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactions_StocksStockId",
                table: "StockTransactions",
                column: "StocksStockId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactions_Stocks_StocksStockId",
                table: "StockTransactions",
                column: "StocksStockId",
                principalTable: "Stocks",
                principalColumn: "StockId");
        }
    }
}
