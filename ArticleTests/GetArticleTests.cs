using AutoMapper;
using Moq;
using Newsfeed.Application.Articles.Queries.GetArticleDetail;
using Newsfeed.Application.Interfaces;
using Newsfeed.Domain.Entities;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ArticleTests
{
    public class GetArticleTests
    {
        [Fact]
        public void Should_get_non_existing_article_and_throw_exception()
        {
            // Arrange
            var mappingMock = new Mock<IMapper>();
            var articleRepositoryMock = new Mock<INewsfeedArticlesRepository>();

            NewsfeedArticle article = null;

            articleRepositoryMock.Setup(s => s.GetArticleByIdAsync(2))
                                 .Returns(Task.FromResult(article));

            var queryHandler = new GetArticleDetailQueryHandler(articleRepositoryMock.Object, mappingMock.Object);

            // Act
            async Task queryResult() => await queryHandler.Handle(new GetArticleDetailQuery { Id = 2 }, new System.Threading.CancellationToken());

            // Assert
            Assert.ThrowsAsync<Exception>(queryResult);
        }
    }
}
