using Xunit;

namespace Wms.Web.Api.ContainerTests;

[CollectionDefinition(Name)]
public class ContainerTestCollection : ICollectionFixture<TestApplication>
{
    public const string Name = "Container test collection";

}