using Microsoft.AspNetCore.Identity;

namespace StockApp.Portal.Areas.Identity.Data
{
	public class ApplicationRole : IdentityRole<Guid>
	{
		public bool IsActive { get; set; }
	}
}
