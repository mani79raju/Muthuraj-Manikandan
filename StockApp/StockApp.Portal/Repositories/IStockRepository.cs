using Microsoft.AspNetCore.Identity;
using StockApp.Portal.Areas.Models;
using StockApp.Portal.ViewModels;

namespace StockApp.Portal.Repositories
{
    public interface IStockRepository
    {
        List<Stocks> GetStocks();
        Stocks GetStockById(string stockId);
        string AddStock(StockViewModel stock);
        string AddStock(List<StockViewModel> stocks);
        string UpdateStock(StockViewModel stock);
        string DeleteStock(string stockId);
        string BuyStock(StockBuyViewModel stock);
        string SellStock(StockSellViewModel stock);

        //Stock Transactions
        List<StockTransactions> GetTransactions(Func<StockTransactions,bool> condition);

        //Stock Quantity
        List<StocksQuantity> GetStockQuantities(Func<StocksQuantity, bool> condition);
        string[] GetInvestmentStatus(string userId, string stockId, double marketPrice);
        string[] GetStockMarketPrice(string stockId, double yesterdayMarketPrice, double todayMarketPrice);
    }
}
