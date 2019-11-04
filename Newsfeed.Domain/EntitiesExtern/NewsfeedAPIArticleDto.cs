using System;
using System.Collections.Generic;

namespace Newsfeed.Domain.Entities
{
    /// <summary>
    /// The exact data that gets returned from the extern news API https://newsapi.org
    /// </summary>
    public class NewsfeedAPIDataDto
    {
        public string Status { get; set; }
        public int TotalResults { get; set; }
        public IEnumerable<NewsfeedAPIArticleDto> Articles { get; set; }
    }

    public class NewsfeedAPIArticleDto{

        public NewsfeedAPIArticleSourceDto Source { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public string UrlToImage { get; set; }

        public DateTimeOffset PublishedAt { get; set; }

        public string Content { get; set; }
    }

    public class NewsfeedAPIArticleSourceDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

    }
}
