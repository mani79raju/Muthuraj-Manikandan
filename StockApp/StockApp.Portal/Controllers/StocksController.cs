using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockApp.Portal.Areas.Identity.Data;
using StockApp.Portal.Options;
using StockApp.Portal.Repositories;
using StockApp.Portal.ViewModels;
using System;

namespace StockApp.Portal.Controllers
{
    public class StocksController : Controller
    {
        IUnitOfWork UnitOfWork { get; set; }
        IOptions Options { get; set; }
        IUserRepository UserRepository { get; set; }

        public StocksController(IUnitOfWork unitOfWork, IOptions options, IUserRepository userRepository)
        {
            UnitOfWork = unitOfWork;
            Options = options;
            UserRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            var stocks = UnitOfWork.Stocks.GetStocks().Select(x => new StockViewModel() { StockId = x.StockId, StockName = x.StockName, StockCode = x.StockCode, Exchange = x.Exchange }).ToList();
            ViewBag.ExchangeTypes = Options.GetStockOptions().Exchanges;
            return View(stocks);
        }

        public IActionResult StocksList()
        {
            var stocks = UnitOfWork.Stocks.GetStocks().Select(x => new StockViewModel() { YesterdayMarketPrice = (double)Random.Shared.Next(1, 1000) / 10, StockId = x.StockId, StockName = x.StockName, StockCode = x.StockCode, Exchange = x.Exchange }).ToList();
            return View(stocks);
        }

        public IActionResult Invested()
        {
            var user = UserRepository.GetUserByEmail(User.Identity.Name);
            var model = UnitOfWork.Stocks.GetStockQuantities(x => x.UserId == user.Id && x.Count > 0).ToList();
            var  stocks = model.Select(x => new InvestedViewModel() { StockId = x.StockId, StockName = x.Stocks.StockName, StockCode = x.Stocks.StockCode, Exchange = x.Stocks.Exchange, AvailableCount = x.Count }).ToList();
            return View(stocks);
        }

        //[HttpPost]
        public string[] GetInvestmentStatus(string stockId)
        {
            var marketPrice = (double)Random.Shared.Next(1, 1000) / 10;
            var user = UserRepository.GetUserByEmail(User.Identity.Name);
            return UnitOfWork.Stocks.GetInvestmentStatus(user.Id, stockId, marketPrice);
        }

        public string[] GetStockMarketPrice(string stockId, string yesterdayMarketPrice)
        {
            var todayMarketPrice = (double)Random.Shared.Next(1, 1000) / 10;
            return UnitOfWork.Stocks.GetStockMarketPrice(stockId, Convert.ToDouble(yesterdayMarketPrice), todayMarketPrice);
        }

        public string GetMarketPrice()
        {
            var todayMarketPrice = (double)Random.Shared.Next(1, 1000) / 10;
            return todayMarketPrice.ToString();
        }

        [Authorize(Policy = "Admin")]
        public void SaveStock(string[] stockNames, string[] stockCodes, string[] stockExchanges, string localDatetime)
        {
            if (stockNames != null && stockNames.Length > 0)
            {
                List<StockViewModel> stocks = new List<StockViewModel>();
                var utcDate = DateTime.UtcNow;
                for (int i = 0; i < stockNames.Length; i++)
                {
                    stocks.Add(new StockViewModel() { 
                        StockName = stockNames[i],
                        StockCode = stockCodes[i],
                        Exchange = stockExchanges[i],
                        AddDateTimeUTC = utcDate,
                        AddDateTimeLocal = localDatetime
                    });
                }
                UnitOfWork.Stocks.AddStock(stocks);
            }
            //return RedirectToAction("Roles");
        }

        public string UpdateStock(StockViewModel model)
        {
            UnitOfWork.Stocks.UpdateStock(model);
            return "Success";
        }

        public string DeleteStock(string stockId)
        {
            UnitOfWork.Stocks.DeleteStock(stockId);
            return "Success";
        }

        public IActionResult Buy(string stockId)
        {
            StockBuyViewModel model = new StockBuyViewModel();
            var stock = UnitOfWork.Stocks.GetStockById(stockId);
            if (stock != null)
            {
                model.StockId = stock.StockId;
                model.StockName = stock.StockName;
                model.StockCode = stock.StockCode;
                model.Exchange = stock.Exchange;
                model.MarketPrice = (double)Random.Shared.Next(1, 1000) / 10;
                model.BuyTypes = Options.GetStockOptions().BuyTypes.Split(',').ToList();
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Buy(StockBuyViewModel stock)
        {
            var user = UserRepository.GetUserByEmail(User.Identity.Name);
            if (stock != null)
            {
                stock.AddDateTimeUTC = DateTime.UtcNow;
                stock.UserId = user.Id;
                UnitOfWork.Stocks.BuyStock(stock);
            }
            return RedirectToAction("Invested");
        }

        public IActionResult Sell(string stockId)
        {
            var user = UserRepository.GetUserByEmail(User.Identity.Name);
            StockSellViewModel model = new StockSellViewModel();
            var stock = UnitOfWork.Stocks.GetStockQuantities(x => x.UserId == user.Id && x.StockId == stockId).FirstOrDefault();
            if (stock != null)
            {
                model.StockId = stock.Stocks.StockId;
                model.StockName = stock.Stocks.StockName;
                model.StockCode = stock.Stocks.StockCode;
                model.Exchange = stock.Stocks.Exchange;
                model.MarketPrice = (double)Random.Shared.Next(1, 1000) / 10;
                model.AvailableCount = stock.Count;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Sell(StockSellViewModel stock)
        {
            var user = UserRepository.GetUserByEmail(User.Identity.Name);
            if (stock != null)
            {
                stock.AddDateTimeUTC = DateTime.UtcNow;
                stock.UserId = user.Id;
                UnitOfWork.Stocks.SellStock(stock);
            }
            return RedirectToAction("Invested");
        }
    }
}
