using Microsoft.AspNetCore.Mvc.Testing;
using Newsfeed.Integration.Testing;
using System.Net.Http;
using Xunit;

namespace Newsfeed.Integration.Tests
{
    public class TestHostFixture : CustomWebApplicationFactory<Api.Startup>
    {
        CustomWebApplicationFactory<Api.Startup> _factory;
        public readonly HttpClient Client;

        public TestHostFixture()
        {
            _factory = new CustomWebApplicationFactory<Api.Startup>();
            Client = _factory.CreateClient();
        }
    }
}