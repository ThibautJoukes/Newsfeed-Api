using System.Threading.Tasks;
using Xunit;

namespace Newsfeed.Integration.Tests.Tests
{
    [Collection("newsfeedTest")]
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

            Assert.Equal("Ping ping, server staat online!", await response.Content.ReadAsStringAsync());
        }
    }
}
