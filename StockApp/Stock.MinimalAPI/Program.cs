using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Stock.MinimalAPI.APIs;
using StockApp.Portal.Areas.Identity.Data;
using StockApp.Portal.Helpers;
using StockApp.Portal.Repositories;
using StockApp.Portal.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

namespace Stock.MinimalAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(connectionString));
            builder.Services.AddScoped<IStockRepository,StockRepository>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });
            //builder.Services.AddControllers().AddJsonOptions(x =>
            //    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            builder.Services.AddAuthorization();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapPost("/security/createToken",
            [AllowAnonymous] (UserModel user) =>
            {
                if (user.userName != "" && user.password != "")
                {
                    var issuer = builder.Configuration["Jwt:Issuer"];
                    var audience = builder.Configuration["Jwt:Audience"];
                    var key = Encoding.ASCII.GetBytes
                    (builder.Configuration["Jwt:Key"]);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Expires = DateTime.UtcNow.AddHours(5),
                        Issuer = issuer,
                        Audience = audience,
                        SigningCredentials = new SigningCredentials
                        (new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha512Signature)
                    };
                    tokenDescriptor.Subject = new ClaimsIdentity(user.claims);
                    
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var jwtToken = tokenHandler.WriteToken(token);
                    var stringToken = tokenHandler.WriteToken(token);
                    return Results.Ok(stringToken);
                }
                return Results.Unauthorized();
            });

            app.RegisterStockMinimalApi();

            //app.MapGet("/GetAllStocks1", (ApplicationDbContext dbContext) => dbContext.Stocks.ToList());//.RequireAuthorization();
            //app.MapGet("/GetMyStocks1/{userId}", (string userId, ApplicationDbContext dbContext) =>
            //{
            //    var model = dbContext.StocksQuantity.Include(x => x.Stocks).Where(x => x.UserId == userId && x.Count > 0).ToList();
            //    var stocks = model.Select(x => new InvestedViewModel() { StockId = x.StockId, StockName = x.Stocks.StockName, StockCode = x.Stocks.StockCode, Exchange = x.Stocks.Exchange, AvailableCount = x.Count }).ToList();
            //    return stocks;
            //});//.RequireAuthorization(Constants.InvestorPolicy);

            app.UseAuthentication();
            app.UseAuthorization();
            app.Run();
        }
    }
}