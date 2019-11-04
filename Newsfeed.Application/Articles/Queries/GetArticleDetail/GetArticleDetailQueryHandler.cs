using AutoMapper;
using MediatR;
using Newsfeed.Application.Interfaces;
using Newsfeed.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Newsfeed.Application.Articles.Queries.GetArticleDetail
{
    public class GetArticleDetailQueryHandler : IRequestHandler<GetArticleDetailQuery, GetArticleDetailVm>
    {
        private readonly INewsfeedArticlesRepository _articles;
        private readonly IMapper _IMapper;

        public GetArticleDetailQueryHandler(INewsfeedArticlesRepository articles, IMapper IMapper)
        {
            _articles = articles;
            _IMapper = IMapper;
        }

        public async Task<GetArticleDetailVm> Handle(GetArticleDetailQuery request, CancellationToken cancellationToken)
        {
            var result = await GetArticleByIdAsync(request.Id);

            if (result == null)
            {
                throw new Exception("No article has been found.");
            }

            return _IMapper.Map<GetArticleDetailVm>(result);
        }

        private async Task<NewsfeedArticle> GetArticleByIdAsync(int id)
        {
            return await _articles.GetArticleByIdAsync(id);
        }

    }
}
