using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace TrollMarket.DataAccess
{
    public class Dependencies
    {
        public static void AddDataAccessServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TrollMarketContext>(
                    options => options.UseSqlServer(configuration.GetConnectionString("TrollMarketConnection"))
                    );
        }
    }
}
