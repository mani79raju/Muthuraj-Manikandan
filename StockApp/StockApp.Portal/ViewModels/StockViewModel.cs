namespace StockApp.Portal.ViewModels
{
    public class StockViewModel
    { 
        public string StockId { get; set; } 
        public string StockName { get; set; }   
        public string StockCode { get; set; }
        public double MarketPrice { get; set; }
        public double YesterdayMarketPrice { get; set; }
        public string StockStatus { get; set; }
        public string Exchange { get; set; }
        public DateTime AddDateTimeUTC { get; set; }
        public string AddDateTimeLocal { get; set; }
    }
}
