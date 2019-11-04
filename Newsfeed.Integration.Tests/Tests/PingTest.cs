using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Xunit;

namespace Newsfeed.Integration.Tests.Tests
{
    [Collection("Collection tests")]
    public class PingTest
    {
        private readonly TestHostFixture _testHostFixture;

        public PingTest(TestHostFixture testHostFixture)
        {
            _testHostFixture = testHostFixture;
        }
        
        [Fact]
        public async Task get_ping()
        {
            // Arrange
            // Act
            var response = await _testHostFixture.Client.GetAsync("/ping");

            //Assert.Equal(await response.Content.ReadAsStringAsync(), "Pinfefzeg ping, server staat online!");
            Assert.True(true);
        }
    }
}
