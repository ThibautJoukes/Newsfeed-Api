using Newsfeed.Application.Articles.Queries.Common;
using System.Collections.Generic;

namespace Newsfeed.Application.Articles.Queries.GetArticleList
{
    public class ArticlesListVm
    {
      public IEnumerable<ArticleLookupDto> Articles { get; set; }
    }
}
