using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using StockApp.Portal.Areas.Identity.Data;
using StockApp.Portal.Areas.Models;
using StockApp.Portal.Repositories;
using StockApp.Portal.ViewModels;

namespace Stock.MinimalAPI.APIs
{
    public static class StockMinimalApi
    {    
        public static void RegisterStockMinimalApi(this WebApplication app)
        {
            app.MapGet("/GetAllStocks", GetAllStocks);
            app.MapGet("/GetMyStocks/{userId}", GetMyStocks);
            app.MapPost("/AddStock", AddStock);
            app.MapPut("/UpdateStock", UpdateStock);
        }

        public static IResult GetAllStocks(IStockRepository stocks)
        {
            var stockList = stocks.GetStocks().Select(x => new StockViewModel() { YesterdayMarketPrice = (double)Random.Shared.Next(1, 1000) / 10, StockId = x.StockId, StockName = x.StockName, StockCode = x.StockCode, Exchange = x.Exchange }).ToList();
            return Results.Ok(stockList);
        }

        public static IResult GetMyStocks(string userId, IStockRepository stocks)
        {
            var stockList = stocks.GetStockQuantities(x => x.UserId == userId).Select(x => new InvestedViewModel() { StockId = x.StockId, StockName = x.Stocks.StockName, StockCode = x.Stocks.StockCode, Exchange = x.Stocks.Exchange, AvailableCount = x.Count }).ToList();
            return Results.Ok(stockList);
        }

        public static IResult AddStock(StockViewModel stock, IStockRepository stocks)
        {
            stocks.AddStock(stock);
            return Results.Created($"/GetAllStocks",null);
        }

        public static IResult UpdateStock(StockViewModel stock, IStockRepository stocks)
        {
            stocks.UpdateStock(stock);
            return Results.Accepted($"/GetAllStocks");
        }
    }
}
