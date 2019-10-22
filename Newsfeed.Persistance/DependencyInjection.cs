using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newsfeed.Persistance.Database;

namespace Newsfeed.Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration, string connectieString)
        {

            services.AddDbContext<DbNewsfeedContext>(options =>
            {
                options.UseSqlServer(connectieString);
            });

            //services.AddScoped<INorthwindDbContext>(provider => provider.GetService<NorthwindDbContext>());

            return services;
        }
    }
}
