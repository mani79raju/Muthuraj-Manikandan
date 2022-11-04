using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockApp.Portal.Areas.Identity.Data;
using StockApp.Portal.Areas.Models;

namespace StockApp.Portal.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Stocks> Stocks { get; set; }
    public DbSet<StockTransactions> StockTransactions { get; set; }
    public DbSet<StocksQuantity> StocksQuantity { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.Entity<Stocks>().HasMany(x => x.StockQuantity).WithOne(x => x.Stocks).HasForeignKey(x => x.StockId);
        builder.Entity<Stocks>().HasMany(x => x.StockTransactions).WithOne(x => x.Stocks).HasForeignKey(x => x.StockId);
        //builder.Entity<ProductMaster>()
        //.HasOne<ProductCategory>(e => e.ProductCategory)
        //.WithMany(g => g.Products)
        //.HasForeignKey(s => s.CategoryId);
    }
}
