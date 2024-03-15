﻿using OzonGrpc.Domain;

namespace OzonGrpc.Api.Dto;

public class UpdateProductDto
{
    public ulong Id { get; set; }
    public string? Name { get; set; }
    public float? Weight { get; set; }
    public ProductCategory? Category { get; set; }
    public long? WarehouseId { get; set; }
}