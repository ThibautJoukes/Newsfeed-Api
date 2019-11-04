using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newsfeed.Domain.Entities;
using Newsfeed.Persistance.Database;

namespace Newsfeed.Integration.Testing
{
    public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(async services =>
            {
                // Remove the app's ApplicationDbContext registration.
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<DbNewsfeedContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add ApplicationDbContext using an in-memory database for testing.
                services.AddDbContext<DbNewsfeedContext>((options, context) =>
                {
                    context.UseInMemoryDatabase("newsFeedDataInMemory");
                });

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database
                // context (ApplicationDbContext).
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<DbNewsfeedContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    // Ensure the database is created.
                    db.Database.EnsureCreated();

                    try
                    {
                        // Seed the database with test data.
                        await InitializeDbForTests(db);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                            "database with test messages. Error: {Message}", ex.Message);
                    }
                }
            });

        }

        public async Task InitializeDbForTests(DbNewsfeedContext db)
        {
            await db.NewsfeedArticle.AddRangeAsync(ArticlesData());
            await db.SaveChangesAsync();
        }

        public async Task ReInitializeDbForTests(DbNewsfeedContext db)
        {
            db.NewsfeedArticle.RemoveRange(db.NewsfeedArticle);
            await InitializeDbForTests(db);
        }

        public static List<NewsfeedArticle> ArticlesData()
        {
            return new List<NewsfeedArticle>()
            {
                new NewsfeedArticle()
                {
                    Id = 1,
                    Author = "a",
                    Content = "aa",
                    Description = "aaa",
                    Title = "Title a",
                    PublishedAt = DateTime.Now,
                    Url = "http://www.example.com",
                    UrlToImage = "http://www.example.com/example.jpg",
                    CurrentSourceId = 0,
                    Source = new NewsfeedArticleSource
                    {
                        Id = null,
                        Name = "Asource"
                    }
                },
                new NewsfeedArticle()
                {
                    Id = 2,
                    Author = "b",
                    Content = "bb",
                    Description = "bbb",
                    Title = "Title b",
                    PublishedAt = DateTime.Now,
                    Url = "http://www.example.com",
                    UrlToImage = "http://www.example.com/example.jpg",
                    CurrentSourceId = 0,
                    Source = new NewsfeedArticleSource
                    {
                        Id = null,
                        Name = "Bsource"
                    }
                },
                new NewsfeedArticle()
                {
                    Id = 3,
                    Author = "c",
                    Content = "cc",
                    Description = "ccc",
                    Title = "Title c",
                    PublishedAt = DateTime.Now,
                    Url = "http://www.example.com",
                    UrlToImage = "http://www.example.com/example.jpg",
                    CurrentSourceId = 0,
                    Source = new NewsfeedArticleSource
                    {
                        Id = null,
                        Name = "Csource"
                    }
                },
            };
        }
    }
}