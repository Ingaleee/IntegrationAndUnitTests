using Infrastructure;
using OzonGrpc.Api.Dto;
using OzonGrpc.Domain;

namespace OzonGrpc.Api.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public ulong Add(CreateProductDto dto)
    {
        if (dto is null)
        {
            return 0;
        }

        var product = new Product
        {
            Name = dto.Name,
            WarehouseId = dto.WarehouseId,
            Weight = dto.Weight,
            Category = dto.Category,
            CreatedUtc = DateTime.UtcNow,
        };

        return _repository.Add(product);
    }

    public bool Update(UpdateProductDto dto)
    {
        if (dto is null)
        {
            return false;
        }

        var product = _repository.GetById(dto.Id);
        if (product is null)
        {
            return false;
        }

        var updatedProduct = new Product()
        {
            Id = dto.Id,
            Name = dto.Name ?? product.Name,
            WarehouseId = dto.WarehouseId ?? product.WarehouseId,
            Weight = dto.Weight ?? product.Weight,
            Category = dto.Category ?? product.Category,
            CreatedUtc = product.CreatedUtc
        };

        return _repository.Update(updatedProduct);
    }

    public ICollection<GetProductDto> Get()
    {
        throw new NotImplementedException();
    }

    public GetProductDto? GetById(ulong id)
    {
        if (id == 0)
        {
            return null;
        }

        var entity = _repository.GetById(id);
        if (entity is null)
        {
            return null;
        }

        var result = new GetProductDto
        {
            Id = entity.Id,
            Name = entity.Name,
            WarehouseId = entity.WarehouseId,
            Weight = entity.Weight,
            Category = entity.Category,
            CreatedUtc = entity.CreatedUtc
        };

        return result;
    }
}