namespace StockApp.Portal.ViewModels
{
    public class StockSellViewModel : StockViewModel
    {
        public string TransactionType { get; set; } = "Sell";
        public int AvailableCount { get; set; }
        public int SellCount { get; set; }
        public string UserId { get; set; }
    }
}
