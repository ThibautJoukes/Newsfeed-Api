using Microsoft.EntityFrameworkCore;
using Newsfeed.Persistance.Configurations;
using Newsfeed.Persistance.Entities;

namespace Newsfeed.Persistance.Database
{
    public class DbNewsfeedContext : DbContext
    {
        public DbSet<NewsfeedArticle> NewsfeedArticle { get; set; }
        public DbSet<NewsfeedArticleSource> NewsfeedArticleSource { get; set; }


        public DbNewsfeedContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration<NewsfeedArticle>(new NewsfeedArticleEntityConfiguration());
        }
    }
}
