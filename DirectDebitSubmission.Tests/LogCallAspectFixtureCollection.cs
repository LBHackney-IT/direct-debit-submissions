using Hackney.Core.Testing.Shared;
using Xunit;

namespace DirectDebitSubmission.Tests
{
    [CollectionDefinition("LogCall collection")]
    public class LogCallAspectFixtureCollection : ICollectionFixture<LogCallAspectFixture>
    { }
}
