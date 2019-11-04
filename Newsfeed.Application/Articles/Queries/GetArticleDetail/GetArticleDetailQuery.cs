using MediatR;

namespace Newsfeed.Application.Articles.Queries.GetArticleDetail
{
    public class GetArticleDetailQuery : IRequest<GetArticleDetailVm>
    {
        public int Id { get; set; }
    }
}