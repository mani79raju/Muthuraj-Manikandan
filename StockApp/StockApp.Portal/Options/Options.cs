using Microsoft.Extensions.Options;

namespace StockApp.Portal.Options
{
    public class Options : IOptions
    {
        public StockOptions StockOptions { get; set; }

        public Options(IOptionsMonitor<StockOptions> stockOptions)
        {
            StockOptions = stockOptions.CurrentValue;    
        }

        public StockOptions GetStockOptions()
        {
            return StockOptions;
        }
    }
}
