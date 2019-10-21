using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Newsfeed.Persistance.Entities
{
    public class NewsfeedArticleSource
    {
        public int? Id { get; set; }
        //public string IdSource { get; set; }
        public string Name { get; set; }
        public List<NewsfeedArticle> Articles {get;set;}
    }
}
