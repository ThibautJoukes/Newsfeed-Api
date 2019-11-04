using Autofac;
using AutofacSerilogIntegration;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newsfeed.Api.ConfigModels;
using Newsfeed.Api.Extensions;
using Newsfeed.Api.Middleware;
using Newsfeed.Api.Modules;
using Newsfeed.Application;
using Newsfeed.Infrastructure;
using Newsfeed.Persistance;
using Newtonsoft.Json;
using Serilog;
using System.Net.Http;

namespace Newsfeed.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
             .SetBasePath(env.ContentRootPath)
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
             .Build();

            _corsPolicy = Configuration.GetSection("CorsPolicy").Get<CorsPolicy>();
        }

        public IConfiguration Configuration { get; }
        private CorsPolicy _corsPolicy { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // set all connectionstring available to the application
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));

            // Configure Cors
            services.AddCors(options =>
            {
                options.AddPolicy(_corsPolicy.PolicyName, builder =>
                {
                    builder.WithOrigins(_corsPolicy.AllowOrigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            // Add's projects DI's
            services.AddPersistence(Configuration, Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>().dbSql);
            services.AddInfrastructure(Configuration);
            services.AddApplication();

            // Add services to the collection.
            services.AddOptions();
        }

        // This gets called after 'ConfigureServices'
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register modules
            builder.RegisterModule(new NewsfeedModule());

            // Injects serilogger into autofac using 'AutofacSerilogIntegration'
            builder.RegisterLogger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // global error exception handling
                app.ConfigureExceptionHandler(Log.Logger);
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            //! Cors enabled 
            app.UseCors(_corsPolicy.PolicyName);

            // Middlewares
            app.UseWhen(context => context.Request.Path.StartsWithSegments("/article/publisharticle") && context.Request.Method == HttpMethod.Post.Method, appBuilder =>
            {
                appBuilder.UseMiddleware<PostArticleMiddleware>();
            });

            app.UseWhen(context => context.Request.Path.StartsWithSegments("/article/publisharticles") && context.Request.Method == HttpMethod.Post.Method, appBuilder =>
            {
                appBuilder.UseMiddleware<PostArticlesMiddleware>();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
