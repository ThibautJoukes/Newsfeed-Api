using AutoMapper;
using MediatR;
using Newsfeed.Application.Articles.Queries.Common;
using Newsfeed.Application.Interfaces;
using Newsfeed.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Newsfeed.Application.Articles.Queries.GetFilteredArticle
{
    public class GetFilteredArticleListQueryHandler : IRequestHandler<GetArticleFilterQuery, GetArticleFilterVm>
    {
        private readonly INewsfeedArticlesRepository _articles;
        private readonly IMapper _IMapper;

        public GetFilteredArticleListQueryHandler(INewsfeedArticlesRepository articles, IMapper IMapper)
        {
            _articles = articles;
            _IMapper = IMapper;
        }

        public async Task<GetArticleFilterVm> Handle(GetArticleFilterQuery request, CancellationToken cancellationToken)
        {
            var filter = new NewsfeedArticleFiltering()
            {
                Author = request.Author,
                Content = request.Content,
                Title = request.Title
            };

            var result = await _articles.StartFilterSearchAsync(filter);

            if (result == null)
            {
                throw new Exception("");
            }

            var vm = new GetArticleFilterVm()
            {
                Articles = _IMapper.Map<IEnumerable<ArticleLookupDto>>(result)
            };

            return vm;
        }
    }
}
