using Xunit;

namespace Wms.Web.Api.IntegrationTests;

[CollectionDefinition(Name)]
public class IntegrationTestCollection : ICollectionFixture<TestApplication>
{
    public const string Name = "Integration test collection";
}