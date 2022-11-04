using System.ComponentModel.DataAnnotations;

namespace StockApp.Portal.Areas.Models
{
    public class StockTransactions
    {
        public StockTransactions()
        {
            StockTransactionId = Guid.NewGuid().ToString();
        }

        [Key]
        public string StockTransactionId { get; set; }
        public string StockId { get; set; }
        public string UserId { get; set; }
        public string TransactionType { get; set; }
        public string? BuyType { get; set; }
        public double BuyPrice { get; set; }
        public double SellPrice { get; set; }
        public int Count { get; set; }
        public DateTime TransactionDateTimeUTC { get; set; }
        public string TransactionDateTimeLocal { get; set; }
        public virtual Stocks Stocks { get; set; }
    }
}
