using Microsoft.EntityFrameworkCore;
using Newsfeed.Persistance.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

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

            modelBuilder.Entity<NewsfeedArticle>(e =>
            {
                e.HasOne<NewsfeedArticleSource>(a => a.Source)
                .WithMany(s => s.Articles)
                .HasForeignKey(e => e.CurrentSourceId);

                e.Property(a => a.Id).UseIdentityColumn();
            });

            modelBuilder.Entity<NewsfeedArticleSource>(e =>
            {
                
            });
        }
    }
}
