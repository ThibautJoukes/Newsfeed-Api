using AutoMapper;
using Newsfeed.Application.Common.Mapping;
using Newsfeed.Domain.Entities;
using System;

namespace Newsfeed.Application.Articles.Queries.GetArticleDetail
{
    public class GetArticleDetailVm : IMapFrom<NewsfeedArticle>
    {
        public int Id { get; set; }

        public int CurrentSourceId { get; set; }

        public ArticleDetailSourceVm Source { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public string UrlToImage { get; set; }

        public DateTimeOffset PublishedAt { get; set; }

        public string Content { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewsfeedArticle, GetArticleDetailVm>().ReverseMap();
            profile.CreateMap<NewsfeedArticleSource, ArticleDetailSourceVm>().ReverseMap();
        }
    }
}
