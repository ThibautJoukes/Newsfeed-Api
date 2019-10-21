using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newsfeed.Persistance.Entities;

namespace Newsfeed.Persistance.Configurations
{
    internal class NewsfeedArticleEntityConfiguration : IEntityTypeConfiguration<NewsfeedArticle>
    {
        public void Configure(EntityTypeBuilder<NewsfeedArticle> builder)
        {
            builder.HasOne<NewsfeedArticleSource>(a => a.Source)
                .WithMany(s => s.Articles)
                .HasForeignKey(e => e.CurrentSourceId);

            builder.Property(a => a.Id).UseIdentityColumn();
        }
    }
}
