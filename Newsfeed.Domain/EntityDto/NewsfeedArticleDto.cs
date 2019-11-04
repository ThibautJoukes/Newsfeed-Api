using System;
using System.Collections.Generic;
using System.Text;

namespace Newsfeed.Domain.EntityDto
{
    public class NewsfeedArticleDto
    {
        public int Id { get; set; }

        public int CurrentSourceId { get; set; }

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
