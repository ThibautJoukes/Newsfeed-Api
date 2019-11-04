using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Newsfeed.Integration.Tests
{
    [CollectionDefinition("newsfeedTest")]
    public class IntegrationTestsCollection  
        : ICollectionFixture<TestHostFixture>
    {
        
        // Its purpose is simply to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.


        // these interfaces will be a single instance object shared with multiple classes.
    }
}
