﻿namespace OzonGrpc.Domain;

public class Product
{
    public ulong Id { get; set; }
    public string Name { get; set; }
    public float Weight { get; set; }
    public decimal Price { get; set; }

    public ProductCategory Category { get; set; }
    public DateTime CreatedUtc { get; set; }
    public long WarehouseId { get; set; }
}