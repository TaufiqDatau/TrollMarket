using TrollMarket.Business.Interfaces;
using TrollMarket.Business.Repositories;
using TrollMarket.DataAccess.Models;
using TrollMarket.Presentation.Web.Models.DTO;

namespace TrollMarket.Presentation.Web.Configuration
{
    public static class ConfigureBusinessService
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IShipmentRepository, ShipmentRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<Cart, CartDTO>();
                cfg.CreateMap<CartDTO, Cart>();
            });
            return services;
        }
    }
}
