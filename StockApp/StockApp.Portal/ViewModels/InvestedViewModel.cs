namespace StockApp.Portal.ViewModels
{
    public class InvestedViewModel : StockViewModel
    {
        public int AvailableCount { get; set; }
        public string BuyType { get; set; }
        public double InvestedPrice { get; set; }
        public string Status { get; set; }
    }
}
