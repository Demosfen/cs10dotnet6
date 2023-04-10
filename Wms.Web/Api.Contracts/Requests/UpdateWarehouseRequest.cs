namespace Wms.Web.Api.Contracts.Requests;

public class UpdateWarehouseRequest
{
    public string? Name { get; set; }
    
    public bool? IsDeleted { get; set; }
}