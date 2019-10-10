using System;

namespace Newsfeed.Application.EntityDtos
{
    public class NewsfeedArticleDto
    {
        public NewsfeedArticleSourceDto Source { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public string UrlToImage { get; set; }

        public DateTimeOffset PublishedAt { get; set; }

        public string Content { get; set; }
    }
}
