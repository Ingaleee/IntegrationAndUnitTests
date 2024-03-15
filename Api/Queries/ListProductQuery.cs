using OzonGrpc.Domain;

namespace Api.Filters;

public class ListProductQuery
{
    public long? WarehouseId { get; set; }
    public ProductCategory? Category { get; set; }
    public DateTime? CreatedUtc { get; set; }
    
    public uint Skip { get; set; }
    public uint? Take { get; set; }
}