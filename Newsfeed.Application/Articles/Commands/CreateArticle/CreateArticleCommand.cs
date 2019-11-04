using MediatR;
using Newsfeed.Domain.Entities;
using System;

namespace Newsfeed.Application.Articles.Commands.CreateArticle
{
    public class CreateArticleCommand : IRequest
    {
        public int Id { get; set; }

        public int CurrentSourceId { get; set; }

        public NewsfeedArticleSource Source { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public string UrlToImage { get; set; }

        public DateTimeOffset PublishedAt { get; set; }

        public string Content { get; set; }
    }
}
