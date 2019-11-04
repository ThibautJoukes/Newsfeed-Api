using MediatR;

namespace Newsfeed.Application.Articles.Queries.GetFilteredArticle
{
    public class GetArticleFilterQuery : IRequest<GetArticleFilterVm>
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
