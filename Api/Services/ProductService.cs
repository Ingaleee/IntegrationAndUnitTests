using Api.Filters;
using OzonGrpc.Infrastructure;
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
            Price = dto.Price,
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
            Price = dto.Price ?? product.Price,
            Category = dto.Category ?? product.Category,
            CreatedUtc = product.CreatedUtc
        };

        return _repository.Update(updatedProduct);
    }

    public IEnumerable<GetProductDto> Get(ListProductQuery query)
    {
        var products = _repository.Get();

        if (query.WarehouseId.HasValue)
        {
            products = products.Where(p => p.WarehouseId == query.WarehouseId.Value);
        }

        if (query.CreatedUtc.HasValue)
        {
            products = products.Where(p => p.CreatedUtc == query.CreatedUtc.Value);
        }

        if (query.Category.HasValue)
        {
            products = products.Where(p => p.Category == query.Category.Value);
        }
        
        if (query.Skip != 0)
        {
            products = products.Skip((int)query.Skip);
        }

        if (query.Take.HasValue)
        {
            products = products.Take((int)query.Take.Value);
        }

        return products.Select(p => new GetProductDto
        {
            Id = p.Id,
            Category = p.Category,
            CreatedUtc = p.CreatedUtc,
            Name = p.Name,
            Price = p.Price,
            WarehouseId = p.WarehouseId,
            Weight = p.Weight
        });
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
            Price = entity.Price,
            Weight = entity.Weight,
            Category = entity.Category,
            CreatedUtc = entity.CreatedUtc
        };

        return result;
    }
}