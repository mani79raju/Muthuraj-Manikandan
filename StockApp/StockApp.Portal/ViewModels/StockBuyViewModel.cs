namespace StockApp.Portal.ViewModels
{
    public class StockBuyViewModel : StockViewModel
    {
        public string TransactionType { get; set; } = "Buy";
        public List<string> BuyTypes { get; set; }
        public string BuyType { get; set; }
        public int BuyCount { get; set; }
        public string UserId { get; set; }
    }
}
