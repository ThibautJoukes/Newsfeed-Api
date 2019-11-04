using AutoMapper;
using Newsfeed.Application.Common.Mapping;
using Newsfeed.Domain.Entities;
using System;

namespace Newsfeed.Application.Articles.Queries.Common
{
    public class ArticleLookupDto : IMapFrom<NewsfeedArticle>
    {
        public int Id { get; set; }

        public int CurrentSourceId { get; set; }

        public ArticleLookupSourceDto Source { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public string UrlToImage { get; set; }

        public DateTimeOffset PublishedAt { get; set; }

        public string Content { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewsfeedArticle, ArticleLookupDto>().ReverseMap();
            profile.CreateMap<NewsfeedArticleSource, ArticleLookupSourceDto>().ReverseMap();
        }
    }
}
