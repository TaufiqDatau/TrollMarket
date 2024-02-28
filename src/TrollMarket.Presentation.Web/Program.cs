using AutoMapper;

using Microsoft.AspNetCore.Authentication.Cookies;
using TrollMarket.DataAccess;
using TrollMarket.DataAccess.Models;
using TrollMarket.Presentation.Web.Configuration;
using TrollMarket.Presentation.Web.Models.DTO;
using TrollMarket.Presentation.Web.Services.API;
using TrollMarket.Presentation.Web.Services.MVC;



namespace TrollMarket.Presentation.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            Dependencies.AddDataAccessServices(builder.Services, builder.Configuration);
            builder.Services.AddBusinessServices();
            builder.Services.AddScoped<APIProductService>();
            builder.Services.AddScoped<APIShipmentService>();
            builder.Services.AddScoped<TransactionService>();
            builder.Services.AddScoped<CartService>();
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<ProfileService>();
            builder.Services.AddScoped<HistoryService>();
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<ShipmentService>();
            

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme) //Cookies Configuration
                .AddCookie(options =>
                {
                    options.Cookie.Name = "AuthenticationTicket";
                    options.LoginPath = "/Login";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    options.AccessDeniedPath = "/Error";
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); //Menggunakan Authentication
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}