using Xunit;

namespace Wms.Web.Api.IntegrationTests;

[CollectionDefinition(Name, DisableParallelization = true)]
public class IntegrationTestCollection : ICollectionFixture<TestApplication>
{
    public const string Name = "Integration test collection";
}