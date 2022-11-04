using System.ComponentModel.DataAnnotations;

namespace StockApp.Portal.Areas.Models
{
    public class StocksQuantity
    {
        public StocksQuantity()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }
        public string StockId { get; set; }
        public string UserId { get; set; }
        public int Count { get; set; }
        public Stocks Stocks { get; set; }
    }
}
