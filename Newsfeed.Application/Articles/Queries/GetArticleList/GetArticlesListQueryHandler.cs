using AutoMapper;
using MediatR;
using Newsfeed.Application.Articles.Queries.Common;
using Newsfeed.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Newsfeed.Application.Articles.Queries.GetArticleList
{
    public class GetArticlesListQueryHandler
        : IRequestHandler<GetArticlesListQuery, ArticlesListVm>
    {
        private readonly INewsfeedArticlesRepository _articles;
        private readonly IMapper _IMapper;
        
        public GetArticlesListQueryHandler(INewsfeedArticlesRepository articles, IMapper IMapper)
        {
            _articles = articles;
            _IMapper = IMapper;
        }

        public async Task<ArticlesListVm> Handle(GetArticlesListQuery request, CancellationToken cancellationToken)
        {
            var result = await _articles.GetAllArticlesAsync();

            if (result == null)
            {
                throw new Exception("");
            }

            var vm = new ArticlesListVm()
            {
                Articles = _IMapper.Map<IEnumerable<ArticleLookupDto>>(result)
            };

            return vm;
        }
    }
}
