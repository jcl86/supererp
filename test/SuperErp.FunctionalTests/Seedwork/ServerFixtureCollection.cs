using Xunit;

namespace SuperErp.Core.FunctionalTests
{
    [CollectionDefinition(nameof(ServerFixtureCollection))]
    public class ServerFixtureCollection : ICollectionFixture<ServerFixture>
    {
    }
}
