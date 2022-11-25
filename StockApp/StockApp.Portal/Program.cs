using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StockApp.Portal.Areas.Identity.Data;
using StockApp.Portal.Helpers;
using StockApp.Portal.Options;
using StockApp.Portal.Repositories;

namespace StockApp.Portal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
                        var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

                                    builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

                                                builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddAuthorization(policy => policy.AddPolicy(Constants.AdminPolicy,options => options.RequireClaim(Constants.AdminClaim)));
            builder.Services.AddAuthorization(policy => policy.AddPolicy(Constants.InvestorPolicy, options => options.RequireClaim(Constants.InvestorClaim)));

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.Configure<StockOptions>(builder.Configuration.GetSection("Stocks"));


            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IStockRepository, StockRepository>();
            builder.Services.AddScoped<IOptions, StockApp.Portal.Options.Options>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
                        app.UseAuthentication();;

            app.UseAuthorization();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}