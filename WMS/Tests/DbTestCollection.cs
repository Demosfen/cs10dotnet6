namespace WMS.Tests;

[CollectionDefinition(Name)]
public class DbTestCollection : ICollectionFixture<TestDatabaseFixture>
{
    public const string Name = "Database collection";
}