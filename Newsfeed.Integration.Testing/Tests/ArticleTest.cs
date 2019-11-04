using Newsfeed.Application.Articles.Commands.CreateArticle;
using Newsfeed.Domain.Entities;
using Newsfeed.Integration.Tests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Newsfeed.Integration.Testing.Tests
{
    [Collection("newsfeedTest")]
    public class ArticleTest
    {
        private readonly TestHostFixture _testHostFixture;

        public ArticleTest(TestHostFixture testHostFixture)
        {
            _testHostFixture = testHostFixture;
        }

        [Fact]
        public async Task Get_all_articles()
        {
            // Arrange
            // Act
            var response = await _testHostFixture.Client.GetAsync("/Article");

            var responseText = await response.Content.ReadAsStringAsync();
                        
            var newsfeedObj = JsonConvert.DeserializeObject<LocalNewsfeedArticles>(responseText);

            Assert.NotNull(newsfeedObj);
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Get_article_by_title()
        {
            // Arrange
            var filter = new NewsfeedArticleFiltering()
            {
                Title = "Title a"
            };

            var content = new StringContent(JsonConvert.SerializeObject(filter), Encoding.UTF8, "application/json");

            // Act
            var response = await _testHostFixture.Client.PostAsync("/Article/filter", content);

            var responseText = await response.Content.ReadAsStringAsync();

            var newsfeedObj = JsonConvert.DeserializeObject<LocalNewsfeedArticles>(responseText);

            Assert.NotNull(newsfeedObj);
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Post_article()
        {
            // Arrange
            var article = new CreateArticleCommand()
            {
                Author = "thib",
                Description = "bla bla bah",
                Content = "Dit is test content voor post article test methode.,?...",
                Title = "testing title",
                Source = new NewsfeedArticleSource
                {
                    Id = null,
                    Name = "hln.be"
                },
                PublishedAt = DateTime.Now,
                Url = "http://www.example.com",
                UrlToImage = "http://www.example.com/example.jpg",
                CurrentSourceId = 0
            };

            var content = new StringContent(JsonConvert.SerializeObject(article), Encoding.UTF8, "application/json");

            // Act
            var response = await _testHostFixture.Client.PostAsync("/Article/publishArticle", content);

            //Assert.NotNull(await response.Content.ReadAsStringAsync());
            Assert.True(response.IsSuccessStatusCode);
        }
    }

    public class LocalNewsfeedArticles
    {
        public IEnumerable<NewsfeedArticle> Articles { get; set; }
    }
}
