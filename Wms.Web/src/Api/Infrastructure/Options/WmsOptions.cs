namespace Wms.Web.Api.Infrastructure.Options;

public sealed class WmsOptions
{
    public const string SectionName = "Wms";
    
    public DbProviderEnum DbProvider { get; set; }
    
    [System.Flags]
    public enum DbProviderEnum : byte
    {
        Unknown = 0b_0000_0000,
        Postgres = 0b_0000_0001,
        Sqlite = 0b_0000_0010
    }
}