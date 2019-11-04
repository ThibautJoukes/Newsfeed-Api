using Newsfeed.Application.Articles.Queries.Common;
using System.Collections.Generic;

namespace Newsfeed.Application.Articles.Queries.GetFilteredArticle
{
    public class GetArticleFilterVm
    {
        public IEnumerable<ArticleLookupDto> Articles { get; set; }
    }
}
