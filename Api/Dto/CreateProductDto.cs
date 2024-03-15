using OzonGrpc.Domain;

namespace OzonGrpc.Api.Dto;

public class CreateProductDto
{
    public string Name { get; set; }
    public float Weight { get; set; }
    
    public decimal Price { get; set; }

    public ProductCategory Category { get; set; }
    public long WarehouseId { get; set; }
}