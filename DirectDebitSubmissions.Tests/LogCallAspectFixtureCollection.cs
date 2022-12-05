using Hackney.Core.Testing.Shared;
using Xunit;

namespace DirectDebitSubmissions.Tests
{
    [CollectionDefinition("LogCall collection")]
    public class LogCallAspectFixtureCollection : ICollectionFixture<LogCallAspectFixture>
    { }
}
