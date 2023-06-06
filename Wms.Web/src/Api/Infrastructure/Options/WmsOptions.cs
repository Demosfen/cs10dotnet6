namespace Wms.Web.Api;

public sealed class WmsOptions
{
    public const string SectionName = "Wms";
    
    public DbProviderEnum DbProvider { get; set; }
    public enum DbProviderEnum
    {
        Unknown = 0,
        Postgres = 1,
        Sqlite = 2
    }
}