﻿namespace Domain;

public class Product
{
    public long Id { get; set; }
    public string Name { get; set; }
    public float Weight { get; set; }
    public ProductCategory Category { get; set; }
    public DateTime CreatedUtc { get; set; }
    public long WarehouseId { get; set; }
}