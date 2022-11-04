using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockApp.Portal.Migrations
{
    public partial class StockTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StocksQuantity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StockId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StocksQuantity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockTransactions",
                columns: table => new
                {
                    StockTransactionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StockId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarketPrice = table.Column<double>(type: "float", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    TransactionDateTimeUTC = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionDateTimeLocal = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransactions", x => x.StockTransactionId);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    StockId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StockName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Exchange = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddDateTimeUTC = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddDateTimeLocal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockTransactionsStockTransactionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StockQuantityId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.StockId);
                    table.ForeignKey(
                        name: "FK_Stocks_StocksQuantity_StockQuantityId",
                        column: x => x.StockQuantityId,
                        principalTable: "StocksQuantity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Stocks_StockTransactions_StockTransactionsStockTransactionId",
                        column: x => x.StockTransactionsStockTransactionId,
                        principalTable: "StockTransactions",
                        principalColumn: "StockTransactionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_StockQuantityId",
                table: "Stocks",
                column: "StockQuantityId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_StockTransactionsStockTransactionId",
                table: "Stocks",
                column: "StockTransactionsStockTransactionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "StocksQuantity");

            migrationBuilder.DropTable(
                name: "StockTransactions");
        }
    }
}
