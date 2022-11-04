using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StockApp.Portal.Areas.Identity.Data;
using StockApp.Portal.Areas.Models;
using StockApp.Portal.ViewModels;
using System.Data;

namespace StockApp.Portal.Repositories
{
    public class StockRepository : IStockRepository
    {
        public ApplicationDbContext Context { get; set; }
        public StockRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public string AddStock(List<StockViewModel> stocks)
        {
            if (stocks != null && stocks.Count > 0)
            {
                foreach (var stock in stocks)
                {
                    Context.Stocks.Add(new Stocks()
                    {
                        StockName = stock.StockName,
                        Exchange = stock.Exchange,
                        StockCode = stock.StockCode,
                        //StockId = Guid.NewGuid().ToString(),
                        AddDateTimeUTC = DateTime.UtcNow,
                        AddDateTimeLocal = stock.AddDateTimeLocal
                    });
                }
                Context.SaveChanges();
            }
            return "Stocks Added";
        }

        public string UpdateStock(StockViewModel stock)
        {
            var stockUpdate = GetStockById(stock.StockId);
            if (stockUpdate != null)
            {
                stockUpdate.StockName = stock.StockName;
                stockUpdate.Exchange = stock.Exchange;
                stockUpdate.StockCode = stock.StockCode;
                Context.SaveChanges();
            }

            return "Stock Updated";
        }

        public string DeleteStock(string stockId)
        {
            var stock = GetStockById(stockId);
            if (stock != null)
            {
                Context.Stocks.Remove(stock);
                Context.SaveChanges();
            }

            return "Role Deleted";
        }

        public string BuyStock(StockBuyViewModel stock)
        {
            if (stock != null)
            {
                Context.StockTransactions.Add(new StockTransactions() { 
                    BuyPrice = stock.MarketPrice,
                    BuyType = stock.BuyType,
                    Count = stock.BuyCount,
                    StockId = stock.StockId,
                    TransactionDateTimeLocal = stock.AddDateTimeLocal,
                    TransactionDateTimeUTC = stock.AddDateTimeUTC,
                    TransactionType = stock.TransactionType,
                    UserId = stock.UserId
                });

                var stockQuantity = Context.StocksQuantity.Where(x => x.UserId == stock.UserId && x.StockId == stock.StockId).FirstOrDefault();
                if (stockQuantity != null)
                {
                    stockQuantity.Count += stock.BuyCount;
                }
                else
                {
                    Context.StocksQuantity.Add(new StocksQuantity()
                    {
                        Count = stock.BuyCount,
                        StockId = stock.StockId,
                        UserId = stock.UserId
                    });
                }
                Context.SaveChanges();
            }
            return "Stocks Added";
        }

        public string SellStock(StockSellViewModel stock)
        {
            if (stock != null)
            {
                Context.StockTransactions.Add(new StockTransactions()
                {
                    SellPrice = stock.MarketPrice,
                    Count = stock.SellCount,
                    StockId = stock.StockId,
                    TransactionDateTimeLocal = stock.AddDateTimeLocal,
                    TransactionDateTimeUTC = stock.AddDateTimeUTC,
                    TransactionType = stock.TransactionType,
                    UserId = stock.UserId
                });

                var stockQuantity = Context.StocksQuantity.Where(x => x.UserId == stock.UserId && x.StockId == stock.StockId).FirstOrDefault();
                if (stockQuantity != null)
                {
                    stockQuantity.Count -= stock.SellCount;
                }
                
                Context.SaveChanges();
            }
            return "Stocks Added";
        }

        public Stocks GetStockById(string stockId)
        {
            return Context.Stocks.Where(x => x.StockId == stockId).FirstOrDefault();
        }

        public List<StocksQuantity> GetStockQuantities(Func<StocksQuantity, bool> condition)
        {
            var ttg = Context.StocksQuantity.Include(x => x.Stocks).ToList().Where(condition).ToList();
            return ttg;
        }

        public List<Stocks> GetStocks()
        {
            return Context.Stocks.ToList();
        }

        public string[] GetInvestmentStatus(string userId, string stockId, double marketPrice)
        {
            var stockQuantity =  Context.StocksQuantity.Where(x => x.StockId == stockId && x.UserId == userId).Select(x => x.Count).FirstOrDefault();
            var transactionPrice = Context.StockTransactions.Where(x => x.StockId == stockId && x.UserId == userId && x.TransactionType == "Buy").Select(x => new { x.BuyPrice, x.SellPrice }).ToList();
            var avgCostPrice = transactionPrice.Select(x => x.BuyPrice).Sum() / stockQuantity;
            var status = (avgCostPrice < marketPrice) ? "Profit" : "Loss";
            return new string[] { marketPrice.ToString(), avgCostPrice.ToString(), status };
        }

        public string[] GetStockMarketPrice(string stockId, double yesterdayMarketPrice, double todayMarketPrice)
        {
            var status = (yesterdayMarketPrice < todayMarketPrice) ? "High" : "Low";
            return new string[] { todayMarketPrice.ToString(), status };
        }

        public List<StockTransactions> GetTransactions(Func<StockTransactions, bool> condition)
        {
            throw new NotImplementedException();
        }

        
    }
}
