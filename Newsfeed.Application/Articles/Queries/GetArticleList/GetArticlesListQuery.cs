using MediatR;

namespace Newsfeed.Application.Articles.Queries.GetArticleList
{
    public class GetArticlesListQuery : IRequest<ArticlesListVm>
    {
    }
}
