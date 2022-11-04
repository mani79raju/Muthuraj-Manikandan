using System.ComponentModel.DataAnnotations;

namespace StockApp.Portal.Areas.Models
{
    public class Stocks
    {
        public Stocks()
        {
            StockId = Guid.NewGuid().ToString();
        }

        [Key]
        public string StockId { get; set; }
        public string StockName { get; set; }
        public string StockCode { get; set; }
        public string Exchange { get; set; }
        public DateTime AddDateTimeUTC { get; set; }
        public string AddDateTimeLocal { get; set; }
        //public virtual StockTransactions StockTransactions { get; set; }
        public List<StocksQuantity> StockQuantity { get; set; }
        public List<StockTransactions> StockTransactions { get; set; }
    }
}
