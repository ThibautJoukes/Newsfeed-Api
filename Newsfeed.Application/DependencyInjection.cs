using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Newsfeed.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            // Will look for a automapper profile class to execute
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
