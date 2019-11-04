using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using Xunit;

namespace Newsfeed.Integration.Tests
{
    public class TestHostFixture : ICollectionFixture<WebApplicationFactory<Api.Startup>>
    {
        public readonly HttpClient Client;

        public TestHostFixture()
        {
            var factory = new WebApplicationFactory<Api.Startup>();
            Client = factory.CreateClient();
        }
    }
}
