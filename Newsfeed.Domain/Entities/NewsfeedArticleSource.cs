using System.Collections.Generic;

namespace Newsfeed.Domain.Entities
{
    public class NewsfeedArticleSource
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public List<NewsfeedArticle> Articles {get;set;}
    }
}
